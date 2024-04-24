using System.Linq.Expressions;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using Legacy.Data.DbContexts;
using Legacy.Data.Entities;

namespace Legacy.Data.Seeds;

public static class CatalogSeeder
{
    public static void Seed(this CatalogDbContext dbContext)
    {
        var fixture = new Fixture();

        // brands
        if (dbContext.Brands.Any())
            return;

        fixture.Customize<Brand>(composer => composer
            .Without(b => b.Id));
        var brands = fixture.CreateMany<Brand>(5).ToList();
        dbContext.AddRange(brands);
        dbContext.SaveChanges();

        // categories
        if (dbContext.Categories.Any())
            return;

        fixture.Customize<Category>(composer => composer
                .Without(c => c.Id)
                .Without(c => c.ParentId));
        fixture.Customizations.Add(
            new StringPropertyTruncateSpecimenBuilder<Category>(c => c.Code, 16));
        var categories = fixture.CreateMany<Category>(5).ToList();
        dbContext.AddRange(categories);
        dbContext.SaveChanges();

        // items
        if (dbContext.Items.Any())
            return;

        fixture.Customize<Item>(composer => composer
            .Without(i => i.Id));
        var items = fixture.CreateMany<Item>(100).ToList();
        dbContext.AddRange(items);
        dbContext.SaveChanges();

        // restrictions
        if (dbContext.Restrictions.Any())
            return;

        fixture.Customize<Restriction>(composer => composer
            .Without(i => i.Id));
        var restrictions = fixture.CreateMany<Item>(5).ToList();
        dbContext.AddRange(restrictions);
        dbContext.SaveChanges();
    }
}

public class StringPropertyTruncateSpecimenBuilder<TEntity>(Expression<Func<TEntity, string>> getter, int length) : ISpecimenBuilder
{
    private readonly PropertyInfo _prop = (PropertyInfo)((MemberExpression)getter.Body).Member;

    public object Create(object request, ISpecimenContext context)
    {
        var propInfo = request as PropertyInfo;

        return propInfo != null && AreEquivalent(propInfo, _prop)
            ? context.Create<string>().Substring(0, length)
            : new NoSpecimen();
    }

    private bool AreEquivalent(PropertyInfo a, PropertyInfo b)
    {
        return a.DeclaringType == b.DeclaringType
               && a.Name == b.Name;
    }
}
