namespace Ecommerce.Catalog.Brands;

public record SkuBrand
{
    public Sku Sku { get; }
    public int BrandId { get; }

    public SkuBrand(Sku sku, int brandId)
    {
        Sku = sku;
        BrandId = brandId;
    }

    public static SkuBrand From(Sku? sku, int? brandId)
    {
        if (sku == null)
            throw new ArgumentNullException(nameof(sku));

        return brandId switch
        {
            null => throw new ArgumentNullException(nameof(brandId)),
            <= 0 => throw new ArgumentOutOfRangeException(nameof(brandId), "Brand id must be a positive number"),
            _ => new SkuBrand(sku.Value, brandId.Value)
        };
    }

    public bool MatchesSku(SkuBrand skuBrand) =>
        Sku == skuBrand.Sku;

    public bool MatchesBrand(SkuBrand skuBrand) =>
        BrandId == skuBrand.BrandId;
}
