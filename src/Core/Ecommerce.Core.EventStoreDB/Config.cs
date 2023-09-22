using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Core.EventStoreDB;

public class EventStoreDBConfig
{
    public string ConnectionString { get; set; } = default!;
}

public record EventStoreDBOptions(
    bool UseInternalCheckpointing = true
);

public static class EventStoreDBConfigExtensions
{
    private const string DefaultConfigKey = "EventStore";

    public static IServiceCollection AddEventStoreDB(
        this IServiceCollection services,
        IConfiguration config,
        EventStoreDBOptions? options = null)
    {
        return services;
    }
}
