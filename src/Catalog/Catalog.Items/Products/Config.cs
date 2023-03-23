using Catalog.Items.Brands;
using Catalog.Items.StockKeepingUnits;
using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Items.Products;

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
        options.Projections.LiveStreamAggregation<Product>();

        // Projections
        // TODO

        // Transformations
        // TODO
    }
}
