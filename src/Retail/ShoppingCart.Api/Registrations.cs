using System.Text.Json;
using Ecommerce.Core.Identities;
using Eventuous;
using Eventuous.Diagnostics.OpenTelemetry;
using Eventuous.EventStore;
using Eventuous.EventStore.Producers;
using Eventuous.EventStore.Subscriptions;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Registrations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ShoppingCart.Api.Infrastructure;
using ShoppingCart.Api.Integrations;
using ShoppingCart.Api.Queries.Carts;
using ShoppingCart.Carts;
using ShoppingCart.Inventories;
using ShoppingCart.Prices;
using ShoppingCart.Products;

#pragma warning disable CS0618 // Type or member is obsolete

namespace ShoppingCart.Api;

public static class Registrations
{
    public static void AddEventuous(this IServiceCollection services, IConfiguration config)
    {
        DefaultEventSerializer.SetDefaultSerializer(
            new DefaultEventSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)));

        // event store (core)
        services.AddEventStoreClient(config["EventStore:ConnectionString"]!);
        services.AddAggregateStore<EsdbEventStore>();

        // command services and functional command services
        services.AddFunctionalService<CartFuncService, CartState>();

        // other internal and core services
        services.AddSingleton<ICombIdGenerator, CombIdGenerator>();
        services.AddSingleton<IProductValidator, ProductValidator>();
        services.AddSingleton<IInventoryChecker, InventoryChecker>();
        services.AddSingleton<IPriceQuoter, PriceQuoter>();

        // subscriptions: checkpoint stores
        services.AddSingleton(Mongo.ConfigureMongo(config));
        services.AddCheckpointStore<MongoCheckpointStore>();

        // subscriptions: projections
        services.AddSubscription<AllStreamSubscription, AllStreamSubscriptionOptions>(
            "UserCartProjections",
            builder => builder
                .UseCheckpointStore<MongoCheckpointStore>()
                .AddEventHandler<UserCartProjection>()
                .WithPartitioningByStream(2));

        // subscriptions: gateways
        services
            .AddGateway<AllStreamSubscription, AllStreamSubscriptionOptions, EventStoreProducer, EventStoreProduceOptions>(
                subscriptionId: "CartsIntegrationSubscription",
                routeAndTransform: CartGateway.Transform);

        // health checks for subscription service
        services
            .AddHealthChecks()
            .AddSubscriptionsHealthCheck("subscriptions", HealthStatus.Unhealthy, new []{"tag"});
    }

    private const string OTelServiceName = "shoppingcart";

    public static void AddTelemetry(this IServiceCollection services)
    {
        var otelEnabled = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT") != null;

        services.AddOpenTelemetry()
            .WithMetrics(
                builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(OTelServiceName))
                        .AddAspNetCoreInstrumentation()
                        .AddEventuous()
                        .AddEventuousSubscriptions()
                        .AddPrometheusExporter();
                    if (otelEnabled) builder.AddOtlpExporter();
                }
            );

        services.AddOpenTelemetry()
            .WithTracing(
                builder =>
                {
                    builder
                        .AddAspNetCoreInstrumentation()
                        .AddGrpcClientInstrumentation()
                        .AddEventuousTracing()
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(OTelServiceName))
                        .SetSampler(new AlwaysOnSampler());

                    if (otelEnabled)
                        builder.AddOtlpExporter();
                    else
                        builder.AddZipkinExporter();
                }
            );
    }
}
