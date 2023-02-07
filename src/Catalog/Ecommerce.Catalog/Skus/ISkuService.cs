namespace Ecommerce.Catalog.Skus;

public interface ISkuService
{
    Task<List<Guid>> FindAllRelatedProducts(Sku sku); // all, even cancelled and pending ones
}
