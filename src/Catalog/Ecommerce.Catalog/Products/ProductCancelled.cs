using Ecommerce.Core.Exceptions;
using Wolverine.Marten;

namespace Ecommerce.Catalog.Products;

public sealed record ProductReadyToBeSold(Guid ProductId, DateTime Now); // event; past tense -- will be emitted

public sealed record ProductCancelled(Guid ProductId, DateTime CancelledAt) // event; past tense
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

public sealed record CancelProduct(Guid ProductId, DateTime CancelledAt) // command; present tense
{
    public static CancelProduct Create(Guid productId, DateTime cancelledAt)
    {
        if (productId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(productId));
        if (cancelledAt == DateTime.MinValue ||
            cancelledAt == DateTime.MaxValue ||
            cancelledAt == default)
            throw new ArgumentOutOfRangeException(nameof(cancelledAt));

        return new CancelProduct(productId, cancelledAt);
    }
}

/// <summary>
/// ref: https://wolverine.netlify.app/guide/durability/marten.html#event-store-cqrs-support
/// </summary>
internal static class CancelProductHandler
{
    [MartenCommandWorkflow]
    public static IEnumerable<object> Handle(CancelProduct command, Product product)
    {
        if (product.Status == ProductStatus.Pending)
        {
            yield return ProductCancelled.Create(product.Id, DateTime.Now);
        }
        else
        {
            throw InvalidStateTransitionException.For<Product, ProductStatus>(
                product.Id,
                nameof(product.Status),
                nameof(ProductStatus.Pending));
        }

        if (product.IsReadyToHaveEventBeEmitted())
        {
            yield return new ProductReadyToBeSold(product.Id, DateTime.Now);
        }
    }
}
