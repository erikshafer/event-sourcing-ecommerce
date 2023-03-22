using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Categories.Categories;

internal static class Config
{
    internal static IServiceCollection AddCategories(this IServiceCollection services)
    {
        // TODO

        return services;
    }

    internal static void ConfigureCategories(this StoreOptions options)
    {
        options.Schema.For<Category>()
            .Index(x => x.ParentId)
            .Index(x => x.Code);
    }
}
