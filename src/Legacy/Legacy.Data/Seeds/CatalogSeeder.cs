using Bogus;
using Legacy.Domain.Entities;

namespace Legacy.Data.Seeds;

public static class CatalogSeeder
{
    private const int MaxBrands = 64;
    private const int MaxCategories = 512;
    private const int MaxItems = 1_024;
    private const int MaxRestrictions = 128;

    public static IEnumerable<Brand> GenerateBrands()
    {
        var id = 0;
        var brandFaker = new Faker<Brand>()
            .UseSeed(100)
            .RuleFor(p => p.Id, f => ++id)
            .RuleFor(p => p.Name, f => f.Company.CompanyName(0))
            .RuleFor(p => p.PrimaryContactName, f => f.Name.FullName());
        var brands = brandFaker.Generate(MaxBrands);
        return brands;
    }

    public static IEnumerable<Category> GenerateCategories()
    {
        var id = 0;
        var categoryFaker = new Faker<Category>()
            .UseSeed(100)
            .RuleFor(p => p.Id, f => ++id)
            .RuleFor(p => p.Name, f => f.Commerce.Department(1))
            .RuleFor(p => p.Code, f => f.Random.AlphaNumeric(8).ToUpper())
            .RuleFor(p => p.Description, f => f.Random.Words(4));
        var categories = categoryFaker.Generate(MaxCategories);
        return categories;
    }

    public static IEnumerable<Item> GenerateItems()
    {
        var id = 0;
        var itemFaker = new Faker<Item>()
            .UseSeed(1_000)
            .RuleFor(p => p.Id, f => ++id)
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
            .Ignore(p => p.Brand)
            .RuleFor(p => p.BrandId, f => f.Random.Number(1, MaxBrands))
            .Ignore(p => p.Category)
            .RuleFor(p => p.CategoryId, f => f.Random.Number(1, MaxBrands));
        var items = itemFaker.Generate(MaxItems);
        return items;
    }

    public static IEnumerable<Restriction> GenerateRestrictions()
    {
        var id = 0;
        var restrictionFaker = new Faker<Restriction>()
            .UseSeed(1_000)
            .RuleFor(p => p.Id, f => ++id)
            .RuleFor(p => p.ItemId, f => f.Random.Number(1, MaxItems))
            .Ignore(p => p.Item)
            .RuleFor(p => p.IsRestricted, f => f.Random.Bool(0.35f))
            .RuleFor(p => p.Reason, f => f.Lorem.Sentence(1, 2));
        var restrictions = restrictionFaker.Generate(MaxRestrictions);
        return restrictions;
    }
}
