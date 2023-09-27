using Eventuous;

namespace Catalog.Products;

public class Product : Aggregate<ProductState>
{
    public async Task Draft(
        Sku sku,
        string code,
        Services.IsSkuAvailable isSkuAvailable)
    {
        EnsureDoesntExist();
        await EnsureSkuAvailable(sku, isSkuAvailable);

        Apply(new ProductEvents.ProductDrafted(sku, code));
    }

    private static async Task EnsureSkuAvailable(Sku sku, Services.IsSkuAvailable isSkuAvailable)
    {
        var skuAvailable = await isSkuAvailable(sku);
        if (skuAvailable is false)
            throw new DomainException($"SKU '{sku}' is not available");
    }
}
