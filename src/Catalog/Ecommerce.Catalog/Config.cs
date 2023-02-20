using Ecommerce.Catalog.Products;
using Ecommerce.Core.Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Catalog;

public static class ModuleConfig
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddMarten(config, opts =>
            {
                opts.ConfigureProducts();
            })
            .AddProducts();
}
