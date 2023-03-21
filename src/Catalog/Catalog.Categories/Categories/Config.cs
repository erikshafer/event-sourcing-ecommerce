using Ecommerce.Core.Marten.Repositories;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Categories.Categories;

internal static class Config
{
    internal static IServiceCollection AddCategories(this IServiceCollection services)
    {
        services.AddMartenRepository<Category>();

        return services;
    }

    internal static void ConfigureCategories(this StoreOptions options)
    {
        // Aggregation
        options.Projections.SelfAggregate<Category>();

        // Indexes
        options.Schema.For<Category>()
            .Index(x => x.ParentId)
            .Index(x => x.Code);
    }
}
