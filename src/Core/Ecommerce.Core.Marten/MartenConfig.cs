using Ecommerce.Core.Configurations;
using Ecommerce.Core.Ids;
using Ecommerce.Core.Marten.Ids;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;
using Wolverine.Marten;

namespace Ecommerce.Core.Marten;

/// <summary>
/// See the referenced GitHub code repository this was borrowed from.
/// This is a great way to set up Marten for event sourcing I've used
/// in multiple production systems.
/// ref: https://github.com/oskardudycz/EventSourcing.NetCore/blob/main/Core.Marten/MartenConfig.cs
/// </summary>
public class MartenConfig
{
    private const string DefaultSchema = "public";
    public string ConnectionString { get; set; } = default!;
    public string WriteModelSchema { get; set; } = DefaultSchema;
    public string ReadModelSchema { get; set; } = DefaultSchema;

    public bool ShouldRecreateDatabase { get; set; } = false;
    public DaemonMode DaemonMode { get; set; } = DaemonMode.Solo;

    public bool UseMetadata { get; set; } = true;
}

public static class MartenConfigExtensions
{
    private const string DefaultConfigKey = "EventStore";

    public static IServiceCollection AddMarten(
        this IServiceCollection services,
        IConfiguration config,
        Action<StoreOptions>? configOptions = null,
        string configKey = DefaultConfigKey) =>
        services.AddMarten(
            config.GetRequiredConfig<MartenConfig>(configKey),
            configOptions);

    public static IServiceCollection AddMarten(
        this IServiceCollection services,
        MartenConfig martenConfig,
        Action<StoreOptions>? configOptions = null)
    {
        services
            .AddScoped<IIdGenerator, MartenIdGenerator>()
            .AddMarten(sp => SetStoreOptions(sp, martenConfig, configOptions))
            .IntegrateWithWolverine()
            .ApplyAllDatabaseChangesOnStartup()
            .AddAsyncDaemon(martenConfig.DaemonMode);

        return services;
    }

    private static StoreOptions SetStoreOptions(
        IServiceProvider serviceProvider,
        MartenConfig martenConfig,
        Action<StoreOptions>? configOptions = null)
    {
        var options = new StoreOptions();
        options.Connection(martenConfig.ConnectionString);
        options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

        var schemaName = Environment.GetEnvironmentVariable("SchemaName");
        options.Events.DatabaseSchemaName = schemaName ?? martenConfig.WriteModelSchema;
        options.DatabaseSchemaName = schemaName ?? martenConfig.ReadModelSchema;

        options.UseDefaultSerialization(
            EnumStorage.AsString,
            nonPublicMembersStorage: NonPublicMembersStorage.All
        );

        if (martenConfig.UseMetadata)
        {
            options.Events.MetadataConfig.CausationIdEnabled = true;
            options.Events.MetadataConfig.CorrelationIdEnabled = true;
            options.Events.MetadataConfig.HeadersEnabled = true;
        }

        configOptions?.Invoke(options);

        return options;
    }
}
