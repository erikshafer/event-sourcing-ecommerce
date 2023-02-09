using Ecommerce.Domain.Values;

namespace Ecommerce.Catalog.Skus;

internal class RandomSkuService : ISkuService
{
    public async Task<List<Guid>> FindAllRelatedProducts(Sku sku)
    {
        var someProductId = Guid.NewGuid();
        var productIds = new List<Guid> { someProductId };
        return await Task.FromResult(productIds);
    }
}
