namespace Ecommerce.Catalog.Products.Drafting;

public record ProductDrafted(Guid ProductId, Sku Sku)
{
    public static ProductDrafted Create(Guid productId, Sku sku)
    {
        if (productId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(productId));
        if (sku == null)
            throw new ArgumentOutOfRangeException(nameof(sku));

        return new ProductDrafted(productId, sku);
    }
}