namespace Ecommerce.Catalog.Brands;

public class RandomLegacyIdLocator : ILegacyIdLocator
{
    public Task<SkuBrandLegacyId> Locate(Sku sku, int brandId)
    {
        var random = new Random();
        var legacyId = random.Next().ToString();
        var result = SkuBrandLegacyId.Create(sku, brandId, legacyId);
        return Task.FromResult(result);
    }
}