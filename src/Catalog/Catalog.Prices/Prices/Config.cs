using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Prices.Prices;

internal static class Config
{
    internal static IServiceCollection AddPrices(this IServiceCollection services)
    {
        services.AddMartenRepository<Price>();

        return services;
    }

    internal static void ConfigurePrices(this StoreOptions options)
    {
        // Snapshots
        options.Projections.SelfAggregate<Price>();

        // Projections
        // TODO

        // Transformations
        // TODO
    }
}
