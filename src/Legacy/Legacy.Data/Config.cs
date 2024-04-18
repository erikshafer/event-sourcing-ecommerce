using Legacy.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Data;

public static class Config
{
    private const string ConnectionStringKey = "LegacyDb";

    public static IServiceCollection AddLegacyData(this IServiceCollection services, IConfiguration config) =>
        services.AddEntityFramework(config);

    private static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration config) =>
        services
            .AddDbContext<OrderingDbContext>(options =>
            {
                var connectionString = config.GetConnectionString(ConnectionStringKey);

                options.UseSqlServer(
                    connectionString,
                    builder => builder.MigrationsHistoryTable("__EFCoreMigrationsHistory"));
            })
            .AddDbContext<CatalogDbContext>(options =>
            {
                var connectionString = config.GetConnectionString(ConnectionStringKey);

                options.UseSqlServer(
                    connectionString,
                    builder => builder.MigrationsHistoryTable("__EFCoreMigrationsHistory"));
            });
}
