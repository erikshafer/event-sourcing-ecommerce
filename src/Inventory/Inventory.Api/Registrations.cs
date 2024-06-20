using System.Text.Json;
using Ecommerce.Core.Identities;
using Eventuous;
using Eventuous.Diagnostics.OpenTelemetry;
using Eventuous.EventStore;
using Eventuous.Postgresql.Subscriptions;
using Inventory.Inventories;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Inventory.Api;

public static class Registrations
{
    private const string OTelServiceName = "inventory";
    private const string PostgresSchemaName = "inventory";

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

        // command services and functional command services
        services.AddFunctionalService<InventoryFuncService, InventoryState>();

        // other internal and core services
        services.AddSingleton<ICombIdGenerator, CombIdGenerator>();
        services.AddSingleton<Services.IsInventoryAvailableBySku>(id => new ValueTask<bool>(true));

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
