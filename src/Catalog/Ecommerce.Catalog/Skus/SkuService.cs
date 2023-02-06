namespace Ecommerce.Catalog.Skus;

public interface ISkuService
{
    Task<List<Guid>> FindAllRelatedProducts(Sku sku); // all, even cancelled and pending ones
}

internal class SkuService : ISkuService
{
    public async Task<List<Guid>> FindAllRelatedProducts(Sku sku)
    {
        var someProductId = Guid.NewGuid();
        var productIds = new List<Guid> { someProductId };
        return await Task.FromResult(productIds);
    }
}