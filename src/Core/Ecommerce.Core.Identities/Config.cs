using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Core.Identities;

public static class Config
{
    public static IServiceCollection AddIdentities(this IServiceCollection services)
    {
        return services;
    }
}
