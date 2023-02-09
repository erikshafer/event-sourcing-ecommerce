using Ecommerce.Domain.Values;

namespace Ecommerce.Catalog.Skus;

public interface ISkuService
{
    /// <summary>
    /// Searches through all Products that have the associated SKU,
    /// no matter the status of said aggregate, such as cancelled or pending.
    /// </summary>
    Task<List<Guid>> FindAllRelatedProducts(Sku sku);
}
