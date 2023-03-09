using Catalog.Items.Products;
using Ecommerce.Core.Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Items;

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
