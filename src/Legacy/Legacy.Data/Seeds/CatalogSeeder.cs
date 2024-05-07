using Bogus;
using Legacy.Data.DbContexts;
using Legacy.Data.Entities;

namespace Legacy.Data.Seeds;

public static class CatalogSeeder
{
    public static void Seed(this CatalogDbContext dbContext)
    {
        // brands
        if (dbContext.Brands.Any())
            return;
        var brands = GenerateBrands();
        dbContext.AddRange(brands);
        dbContext.SaveChanges();

        // categories
        if (dbContext.Categories.Any())
            return;
        var categories = GenerateCategories();
        dbContext.AddRange(categories);
        dbContext.SaveChanges();

        // items
        if (dbContext.Items.Any())
            return;
        var items = GenerateItems();
        dbContext.AddRange(items);
        dbContext.SaveChanges();

        // restrictions
        if (dbContext.Restrictions.Any())
            return;
        var restrictions = GenerateRestrictions();
        dbContext.AddRange(restrictions);
        dbContext.SaveChanges();
    }

    private static List<Restriction> GenerateRestrictions()
    {
        var restrictionFaker = new Faker<Restriction>()
            .RuleFor(p => p.ItemId, f => f.Random.Number(1001, 1999))
            .RuleFor(p => p.IsRestricted, f => f.Random.Bool(0.35f))
            .RuleFor(p => p.Reason, f => f.Lorem.Sentence(1, 2));
        var restrictions = restrictionFaker.Generate(128);
        return restrictions;
    }

    private static List<Item> GenerateItems()
    {
        var itemFaker = new Faker<Item>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription()[..256])
            .RuleFor(p => p.Color, f => f.Commerce.Color())
            .RuleFor(p => p.BulletPoint1, f => f.Commerce.ProductDescription()[..32])
            .RuleFor(p => p.BulletPoint2, f => f.Commerce.ProductDescription()[..32])
            .RuleFor(p => p.BulletPoint3, f => f.Commerce.ProductDescription()[..32])
            .RuleFor(p => p.Picture1Url, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Picture2Url, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Picture3Url, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Length, f => f.Random.Number(1, 48))
            .RuleFor(p => p.Width, f => f.Random.Number(1, 48))
            .RuleFor(p => p.Height, f => f.Random.Number(1, 48))
            .RuleFor(p => p.Weight, f => f.Random.Number(1, 99))
            .RuleFor(p => p.Discontinued, f => f.Random.Bool(0.10f))
            .RuleFor(p => p.IsVariant, f => f.Random.Bool(0.10f))
            .RuleFor(p => p.ChildCouldChokeWarning, f => f.Random.Bool(0.02f));
        var items = itemFaker.Generate(1_024);
        return items;
    }

    private static List<Category> GenerateCategories()
    {
        var categoryFaker = new Faker<Category>()
            .RuleFor(p => p.Name, f => f.Commerce.Department())
            .RuleFor(p => p.Code, f => f.Random.AlphaNumeric(8).ToUpper())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
        var categories = categoryFaker.Generate(512);
        return categories;
    }

    private static List<Brand> GenerateBrands()
    {
        var brandFaker = new Faker<Brand>()
            .RuleFor(p => p.Name, f => f.Company.CompanyName())
            .RuleFor(p => p.PrimaryContactName, f => f.Name.FullName());
        var brands = brandFaker.Generate(64);
        return brands;
    }
}
