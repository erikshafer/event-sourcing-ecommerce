using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Brands;

internal static class Config
{
    internal static IServiceCollection AddBrands(this IServiceCollection services)
    {
        // TODO

        return services;
    }

    internal static void ConfigureBrands(this StoreOptions options)
    {
        // TODO
    }
}
