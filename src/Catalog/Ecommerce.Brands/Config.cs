using Ecommerce.Brands.Brands;
using Ecommerce.Core.Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Brands;

public static class ModuleConfig
{
    public static IServiceCollection AddBrandsModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddMarten(config, opts =>
            {
                opts.ConfigureBrands();
            })
            .AddBrands();
}
