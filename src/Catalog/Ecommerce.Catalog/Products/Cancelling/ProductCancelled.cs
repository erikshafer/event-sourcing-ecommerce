namespace Ecommerce.Catalog.Products.Cancelling;

public record ProductCancelled(Guid ProductId, DateTime CancelledAt)
{
    public static ProductCancelled Create(Guid productId, DateTime cancelledAt)
    {
        if (productId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(productId));
        if (cancelledAt == DateTime.MinValue ||
            cancelledAt == DateTime.MaxValue ||
            cancelledAt == default)
            throw new ArgumentOutOfRangeException(nameof(cancelledAt));

        return new ProductCancelled(productId, cancelledAt);
    }
}