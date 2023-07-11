using Ecommerce.Core.Marten;
using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory;

public static class Config
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddMarten(config, opts =>
            {
                opts.ConfigurePrices();
            })
            .AddPrices();

    internal static IServiceCollection AddPrices(this IServiceCollection services)
    {
        services.AddMartenRepository<Inventory>();

        return services;
    }

    internal static void ConfigurePrices(this StoreOptions options)
    {
        // Snapshots
        options.Projections.LiveStreamAggregation<Inventory>();

        // Projections
        // TODO

        // Transformations
        // TODO
    }
}
