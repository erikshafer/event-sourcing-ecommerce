using System.Text.Json;
using Ecommerce.Core.Identities;
using Eventuous;
using Eventuous.Diagnostics.OpenTelemetry;
using Eventuous.EventStore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

#pragma warning disable CS0618 // Type or member is obsolete

namespace ShoppingCart.Api;

public static class Registrations
{
    private const string OTelServiceName = "shoppingcart";
    private const string PostgresSchemaName = "shoppingcart";

    public static void AddEventuous(this IServiceCollection services, IConfiguration configuration)
    {
        DefaultEventSerializer.SetDefaultSerializer(
            new DefaultEventSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            )
        );

        // register known event types (e.g. using [EventType] annotation)
        TypeMap.RegisterKnownEventTypes();

        // event store (core)
        services.AddEventStoreClient(configuration["EventStore:ConnectionString"]!);
        services.AddAggregateStore<EsdbEventStore>();

        // command services (functional services in this module)
        services.AddFunctionalService<CartFuncService, CartState>();

        // other internal and core services
        services.AddSingleton<ISnowflakeIdGenerator, SnowflakeIdGenerator>();

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
