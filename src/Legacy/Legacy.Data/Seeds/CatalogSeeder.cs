using Bogus;
using Legacy.Data.DbContexts;
using Legacy.Data.Entities;

namespace Legacy.Data.Seeds;

public static class CatalogSeeder
{
    private const int MaxBrands = 64;
    private const int MaxCategories = 512;
    private const int MaxItems = 1_024;
    private const int MaxRestrictions = 128;

    public static void Seed(this CatalogDbContext dbContext)
    {
        // brands
        if (dbContext.Brands.Any() is false)
        {
            var brands = GenerateBrands();
            dbContext.AddRange(brands);
            dbContext.SaveChanges();
        }

        // categories
        if (dbContext.Categories.Any() is false)
        {

            var categories = GenerateCategories();
            dbContext.AddRange(categories);
            dbContext.SaveChanges();
        }

        // items
        if (dbContext.Items.Any() is false)
        {
            var items = GenerateItems();
            dbContext.AddRange(items);
            dbContext.SaveChanges();
        }

        // restrictions
        if (dbContext.Restrictions.Any() is false)
        {
            var restrictions = GenerateRestrictions();
            dbContext.AddRange(restrictions);
            dbContext.SaveChanges();
        }
    }

    private static IEnumerable<Brand> GenerateBrands()
    {
        var brandFaker = new Faker<Brand>()
            .RuleFor(p => p.Name, f => f.Company.CompanyName(0))
            .RuleFor(p => p.PrimaryContactName, f => f.Name.FullName());
        var brands = brandFaker.Generate(MaxBrands);
        return brands;
    }

    private static IEnumerable<Category> GenerateCategories()
    {
        var categoryFaker = new Faker<Category>()
            .RuleFor(p => p.Name, f => f.Commerce.Department(1))
            .RuleFor(p => p.Code, f => f.Random.AlphaNumeric(8).ToUpper())
            .RuleFor(p => p.Description, f => f.Random.Words(4));
        var categories = categoryFaker.Generate(MaxCategories);
        return categories;
    }

    private static IEnumerable<Item> GenerateItems()
    {
        var itemFaker = new Faker<Item>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Color, f => f.Commerce.Color())
            .RuleFor(p => p.BulletPoint1, f => f.Commerce.ProductDescription()[..24])
            .RuleFor(p => p.BulletPoint2, f => f.Commerce.ProductDescription()[..24])
            .RuleFor(p => p.BulletPoint3, f => f.Commerce.ProductDescription()[..24])
            .RuleFor(p => p.Picture1Url, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Picture2Url, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Picture3Url, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Length, f => f.Random.Number(1, 48))
            .RuleFor(p => p.Width, f => f.Random.Number(1, 48))
            .RuleFor(p => p.Height, f => f.Random.Number(1, 48))
            .RuleFor(p => p.Weight, f => f.Random.Number(1, 99))
            .RuleFor(p => p.Discontinued, f => f.Random.Bool(0.10f))
            .RuleFor(p => p.IsVariant, f => f.Random.Bool(0.10f))
            .RuleFor(p => p.ChildCouldChokeWarning, f => f.Random.Bool(0.02f))
            // .Ignore(p => p.Brand)
            .RuleFor(p => p.BrandId, f => f.Random.Number(1, MaxBrands))
            // .Ignore(p => p.Category)
            .RuleFor(p => p.CategoryId, f => f.Random.Number(1, MaxBrands));
        var items = itemFaker.Generate(MaxItems);
        return items;
    }

    private static IEnumerable<Restriction> GenerateRestrictions()
    {
        var restrictionFaker = new Faker<Restriction>()
            .RuleFor(p => p.ItemId, f => f.Random.Number(1, MaxItems))
            .RuleFor(p => p.IsRestricted, f => f.Random.Bool(0.35f))
            .RuleFor(p => p.Reason, f => f.Lorem.Sentence(1, 2));
        var restrictions = restrictionFaker.Generate(MaxRestrictions);
        return restrictions;
    }
}
