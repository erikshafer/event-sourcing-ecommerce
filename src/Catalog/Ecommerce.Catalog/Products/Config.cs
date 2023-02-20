using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Catalog.Products;

internal static class Config
{
    internal static IServiceCollection AddProducts(this IServiceCollection services)
    {
        services.AddMartenRepository<Product>();

        return services;
    }

    internal static void ConfigureProducts(this StoreOptions options)
    {
        // Snapshots
        options.Projections.SelfAggregate<Product>();

        // Projections
        // TODO

        // Transformations
        // TODO
    }
}
