using Catalog.Brands;
using Catalog.Categories;
using Catalog.Prices;
using Catalog.Products;
using Ecommerce.Core.Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog;

public static class Config
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddMarten(config, opts =>
            {
                opts.ConfigureProducts();
                opts.ConfigurePrices();
                opts.ConfigureCategories();
                opts.ConfigureBrands();
            })
            .AddProducts()
            .AddPrices()
            .AddCategories()
            .AddBrands();
}
