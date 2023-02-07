namespace Ecommerce.Catalog.Brands;

public interface ILegacyIdLocator
{
    Task<SkuBrandLegacyId> Locate(Sku sku, int brandId);
}