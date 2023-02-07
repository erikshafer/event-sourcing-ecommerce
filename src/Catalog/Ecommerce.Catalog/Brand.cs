namespace Ecommerce.Catalog;

public record Brand(BrandId BrandId, string Name)
{
    public Brand(ulong brandId, string name) : this(new BrandId(brandId), name)
    {
    }
}

public record BrandId(ulong Id)
{
    public BrandId(string id) : this(ulong.Parse(id))
    {
    }
}
