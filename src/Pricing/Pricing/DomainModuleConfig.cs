using EventStore.Client;
using MicroPlumberd.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pricing.Prices;

namespace Pricing;

/// <summary>
/// I like the idea of something abbreviated "DMC".
/// Long name though, but perhaps not a bad norm for project
/// or future projects?
/// Gotta run the DMC... =)
/// </summary>
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
        services.AddMicroPlumberd(config);
        return services;
    }

    private static IServiceCollection AddMicroPlumberd(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["EventStore:ConnectionString"]!;
        var settings = EventStoreClientSettings.Create(connectionString);
        return services
            .AddPlumberd(settings)
            .AddEventHandler<PriceModel>();
    }
}
