using Legacy.Monolith.Orders.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Monolith.Orders;

public static class Config
{
    public static IServiceCollection AddLegacyOrdersModule(this IServiceCollection services, IConfiguration config) =>
        services.AddEntityFramework(config);

    private static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration config) =>
        services
            .AddDbContext<OrderingDbContext>(options =>
            {
                const string schemaName = "monolith";
                var connectionString = config.GetConnectionString("LegacyDatabase");

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
