using Ecommerce.Core.Marten;
using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pricing;

public static class ModuleConfig
{
    public static IServiceCollection AddPricingModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddMarten(config, opts =>
            {
                opts.ConfigurePrices();
            })
            .AddPrices();

    internal static IServiceCollection AddPrices(this IServiceCollection services)
    {
        services.AddMartenRepository<Price>();

        return services;
    }

    internal static void ConfigurePrices(this StoreOptions options)
    {
        // Snapshots
        options.Projections.LiveStreamAggregation<Price>();

        // Projections
        // TODO

        // Transformations
        // TODO
    }
}
