using Catalog.Categories.Categories;
using Ecommerce.Core.Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Categories;

public static class Config
{
    public static IServiceCollection AddCategoriesModule(this IServiceCollection services, IConfiguration config) =>
        services.AddMarten(config, opts =>
            {
                opts.ConfigureCategories();
            })
            .AddCategories();
}
