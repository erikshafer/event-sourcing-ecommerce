using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Brands.Brands;

internal static class Config
{
    internal static IServiceCollection AddBrands(this IServiceCollection services)
    {
        services.AddMartenRepository<Brand>();

        return services;
    }

    internal static void ConfigureBrands(this StoreOptions options)
    {
        // Snapshots
        options.Projections.SelfAggregate<Brand>();

        // Projections
        // TODO

        // Transformations
        // TODO
    }
}
