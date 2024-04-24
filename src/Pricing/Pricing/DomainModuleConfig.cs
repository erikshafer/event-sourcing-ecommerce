using EventStore.Client;
using MicroPlumberd.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pricing;

public static class DomainModuleConfig
{
    /// <summary>
    /// Very much a work in progress (WIP).
    /// Like everything else. =)
    /// Excited to learn more about MicroPlumberd!
    /// </summary>
    public static IServiceCollection AddPricingModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        var connectionString = config["EventStore:ConnectionString"]!;
        var settings = EventStoreClientSettings.Create(connectionString);
        return services.AddPlumberd(settings);
    }
}
