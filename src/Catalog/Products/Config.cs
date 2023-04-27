using Ecommerce.Core.Marten;
using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Products;

public static class ModuleConfig
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddMarten(config, opts =>
            {
                opts.ConfigureProducts();
            })
            .AddProducts();

    internal static IServiceCollection AddProducts(this IServiceCollection services)
    {
        services.AddMartenRepository<Product>();

        services.AddScoped<ISkuValidatorService, SkuValidatorService>();

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
