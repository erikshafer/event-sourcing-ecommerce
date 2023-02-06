namespace Ecommerce.Catalog.Products.Confirming;

public record ProductConfirmed(Guid ProductId, DateTime ConfirmedAt)
{
    public static ProductConfirmed Create(Guid productId, DateTime confirmedAt)
    {
        if (productId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(productId));
        if (confirmedAt == DateTime.MinValue ||
            confirmedAt == DateTime.MaxValue ||
            confirmedAt == default)
            throw new ArgumentOutOfRangeException(nameof(confirmedAt));

        return new ProductConfirmed(productId, confirmedAt);
    }
}