using JasperFx.Core;
using Marten.Exceptions;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Oakton;
using Wolverine;
using Wolverine.ErrorHandling;

namespace Ecommerce.Core.Wolverine;

public static class Config
{
    public static IHostBuilder AddWolverineWithDefaultConfig(this IHostBuilder builder)
    {
        builder.ApplyOaktonExtensions();

        builder.UseWolverine(opts =>
        {
            // If we encounter a concurrency exception, just try it immediately
            // up to 3 times total
            opts.Handlers.OnException<ConcurrencyException>().RetryTimes(3);

            // It's an imperfect world, and sometimes transient connectivity errors
            // to the database happen
            opts.Handlers.OnException<NpgsqlException>()
                .RetryWithCooldown(50.Milliseconds(), 100.Milliseconds(), 250.Milliseconds());
        });

        return builder;
    }
}
