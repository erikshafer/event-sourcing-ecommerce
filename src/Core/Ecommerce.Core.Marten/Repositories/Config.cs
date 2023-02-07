using Ecommerce.Core.Aggregates;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Core.Marten.Repositories;

public static class Config
{
    public static IServiceCollection AddMartenRepository<T>(
        this IServiceCollection services,
        bool withAppendScope = true,
        bool withTelemetry = true)
        where T : class, IAggregate
    {
        services.AddScoped<IMartenRepository<T>, MartenRepository<T>>();

        // TODO if withAppendScope

        // TODO if withTelemetry

        return services;
    }
}
