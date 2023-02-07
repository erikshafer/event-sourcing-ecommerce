namespace Ecommerce.Catalog.Brands;

public record SkuBrandLegacyId
{
    public Sku Sku => SkuBrand.Sku;
    
    public int BrandId => SkuBrand.BrandId;

    public string LegacyId { get; }

    public SkuBrand SkuBrand { get; }

    private SkuBrandLegacyId(SkuBrand skuBrand, string legacyId)
    {
        SkuBrand = skuBrand;
        LegacyId = legacyId;
    }

    public static SkuBrandLegacyId Create(Sku sku, int? brandId, string legacyId) => 
        Create(
            SkuBrand.From(sku, brandId), 
            legacyId);

    public static SkuBrandLegacyId Create(SkuBrand skuBrand, string legacyId)
    {
        if (string.IsNullOrWhiteSpace(legacyId))
            throw new ArgumentOutOfRangeException(nameof(legacyId));
        
        return new SkuBrandLegacyId(skuBrand, legacyId);
    }

    public bool Matches(SkuBrandLegacyId skuBrandLegacyId) => 
        SkuBrand == skuBrandLegacyId.SkuBrand &&
        LegacyId == skuBrandLegacyId.LegacyId;
}