using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Products;

internal static class Config
{
    internal static IServiceCollection AddProducts(this IServiceCollection services)
    {
        // TODO

        return services;
    }

    internal static void ConfigureProducts(this StoreOptions options)
    {
        // TODO
    }
}
