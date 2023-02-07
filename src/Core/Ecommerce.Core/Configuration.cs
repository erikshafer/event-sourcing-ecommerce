using Ecommerce.Core.Ids;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Core;

public static class Configuration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.TryAddScoped<IIdGenerator, NulloIdGenerator>();

        return services;
    }
}
