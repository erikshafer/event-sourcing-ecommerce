using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.LegacyCatalog;

public static class Config
{
    public static IServiceCollection AddLegacyCatalogModule(this IServiceCollection services, IConfiguration config) =>
        services.AddEntityFramework(config);

    private static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration config) =>
        services.AddDbContext<LegacyCatalogDbContext>(options =>
        {
            const string schemaName = "SchemaName";
            var connectionString = config.GetConnectionString("LegacyCatalogDatabase");
            options.UseSqlServer(
                connectionString,
                builder => builder.MigrationsHistoryTable("__EFCoreMigrationsHistory", schemaName.ToLower()));
        });
}

public class LegacyCatalogDbContext : DbContext
{
    public LegacyCatalogDbContext(DbContextOptions<LegacyCatalogDbContext> options)
        : base(options)
    {
    }

    // TODO: entities
}
