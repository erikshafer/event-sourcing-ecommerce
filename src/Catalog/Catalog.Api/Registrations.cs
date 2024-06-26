using System.Text.Json;
using Catalog.Api.Commands.Offers;
using Catalog.Api.Commands.Prices;
using Catalog.Api.Commands.Products;
using Catalog.Api.Infrastructure;
using Catalog.Api.Queries.Offers;
using Catalog.Api.Queries.Prices;
using Catalog.Api.Queries.Products;
using Catalog.Offers;
using Catalog.Prices;
using Catalog.Products;
using Ecommerce.Core.Identities;
using Eventuous;
using Eventuous.Diagnostics.OpenTelemetry;
using Eventuous.EventStore;
using Eventuous.EventStore.Subscriptions;
using Eventuous.Postgresql.Subscriptions;
using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Registrations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ThirdParty.BouncyCastle.Utilities.IO.Pem;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Catalog.Api;

public static class Registrations
{
    private const string OTelServiceName = "catalog";

    public static void AddEventuous(this IServiceCollection services, IConfiguration configuration)
    {
        DefaultEventSerializer.SetDefaultSerializer(
            new DefaultEventSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            )
        );

        // event store (core)
        services.AddEventStoreClient(configuration["EventStore:ConnectionString"]!);
        services.AddAggregateStore<EsdbEventStore>();

        // command services
        services.AddCommandService<ProductCommandService, Product>();
        services.AddCommandService<PriceCommandService, Price>();
        services.AddCommandService<OfferCommandService, Offer>();

        // other internal and core services
        services.AddSingleton<ICombIdGenerator, CombIdGenerator>();
        services.AddSingleton<Catalog.Products.Services.IsSkuAvailable>(id => new ValueTask<bool>(true));
        services.AddSingleton<Catalog.Products.Services.IsUserAuthorized>(id => new ValueTask<bool>(true));
        services.AddSingleton<Catalog.Prices.Services.IsSkuAvailable>(id => new ValueTask<bool>(true));
        services.AddSingleton<Catalog.Prices.Services.IsUserAuthorized>(id => new ValueTask<bool>(true));
        services.AddSingleton<Catalog.Offers.Services.IsSkuAvailable>(id => new ValueTask<bool>(true));
        services.AddSingleton<Catalog.Offers.Services.IsUserAuthorized>(id => new ValueTask<bool>(true));

        // event store related

        // subscriptions: checkpoint stores
        services.AddSingleton(Mongo.ConfigureMongo(configuration));
        services.AddCheckpointStore<MongoCheckpointStore>();

        // services.AddEventuousPostgres(); // for PG ES
        // services.AddSingleton(Postgres.ConfigurePostgres(configuration));
        // services.AddCheckpointStore<PostgresCheckpointStore>();

        // subscriptions: projections
        services.AddSubscription<AllStreamSubscription, AllStreamSubscriptionOptions>(
            "ProductsProjections",
            builder => builder
                .UseCheckpointStore<MongoCheckpointStore>()
                .AddEventHandler<ProductStateProjection>()
                .WithPartitioningByStream(2));

        services.AddSubscription<AllStreamSubscription, AllStreamSubscriptionOptions>(
            "PricesProjections",
            builder => builder
                .UseCheckpointStore<MongoCheckpointStore>()
                .AddEventHandler<PriceStateProjection>()
                .WithPartitioningByStream(2));

        services.AddSubscription<AllStreamSubscription, AllStreamSubscriptionOptions>(
            "OffersProjections",
            builder => builder
                .UseCheckpointStore<MongoCheckpointStore>()
                .AddEventHandler<OfferStateProjection>()
                .WithPartitioningByStream(2));

        // TODO: add additional mongo, postgresql, and other custom projections

        // services.AddSubscription<AllStreamSubscription, AllStreamSubscriptionOptions>(
        //     "ProductDraftsProjections",
        //     builder => builder
        //         .UseCheckpointStore<PostgresCheckpointStore>()
        //         .AddEventHandler<ProductPgProjector>()
        //         .WithPartitioningByStream(2));

        // subscriptions: persistent subscriptions
        // TODO: Add persistent subscription for integration points and other use cases

        // health checks for subscription service
        services
            .AddHealthChecks()
            .AddSubscriptionsHealthCheck("subscriptions", HealthStatus.Unhealthy, new []{"tag"});
    }

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
                        .AddMongoDBInstrumentation()
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
