using Legacy.Monolith.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Monolith;

public static class Config
{
    public static IServiceCollection AddLegacyCatalogModule(this IServiceCollection services, IConfiguration config) =>
        services.AddEntityFramework(config);

    private static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration config) =>
        services.AddDbContext<CatalogDbContext>(options =>
        {
            const string schemaName = "catalog";
            var connectionString = config.GetConnectionString("LegacyCatalogDatabase");
            options.UseSqlServer(
                connectionString,
                builder => builder.MigrationsHistoryTable("__EFCoreMigrationsHistory", schemaName.ToLower()));
        });

    public static void ConfigureLegacyCatalogModule(this IApplicationBuilder app)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        // if (environment == "Development")
        // {
        //     using var serviceScope = app.ApplicationServices.CreateScope();
        //     serviceScope.ServiceProvider.GetRequiredService<LegacyCatalogDbContext>().Database.Migrate();
        // }
    }
}
