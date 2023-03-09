using Ecommerce.Core.Marten;
using Catalog.Prices.Prices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Prices;

public static class ModuleConfig
{
    public static IServiceCollection AddPricesModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddMarten(config, opts =>
            {
                opts.ConfigurePrices();
            })
            .AddPrices();
}
