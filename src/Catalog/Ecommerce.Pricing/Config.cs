using Ecommerce.Core.Marten;
using Ecommerce.Pricing.Prices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Pricing;

public static class ModuleConfig
{
    public static IServiceCollection AddPricingModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddMarten(config, opts =>
            {
                opts.ConfigurePrices();
            })
            .AddPrices();
}
