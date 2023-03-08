using Ecommerce.Catalog.Brands;
using Ecommerce.Catalog.StockKeepingUnits;
using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Catalog.Products;

internal static class Config
{
    internal static IServiceCollection AddProducts(this IServiceCollection services)
    {
        services.AddMartenRepository<Product>();

        services.AddScoped<ISkuValidatorService, SkuValidatorService>();
        services.AddScoped<IBrandValidatorService, BrandValidatorService>();

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
