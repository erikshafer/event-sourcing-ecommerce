using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Application;

/*
 *
 * Honestly, may make the legacy projects extremely simple
 * in interest of time and not have a domain-like project.
 * Or just have it be a few extension methods and call it
 * a day.
 *
 */

public static class Config
{
    public static IServiceCollection AddLegacyDomain(this IServiceCollection services)
    {
        return services;
    }
}
