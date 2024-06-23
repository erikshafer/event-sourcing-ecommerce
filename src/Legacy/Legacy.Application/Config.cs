using Legacy.Application.Services.Catalog;
using Legacy.Application.Services.Inventory;
using Microsoft.Extensions.DependencyInjection;

namespace Legacy.Application;

public static class Config
{
    public static IServiceCollection AddLegacyApplication(this IServiceCollection services)
    {
        /* the mediatr library */
        services.AddMediatR(opts =>
        {
            opts.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        /* application services */
        services.AddTransient<IInventoryService, InventoryService>();
        services.AddTransient<ICatalogService, CatalogService>();

        return services;
    }
}
