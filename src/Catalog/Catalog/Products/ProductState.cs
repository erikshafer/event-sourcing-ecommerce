using Eventuous;

using static Catalog.Products.ProductEvents;

namespace Catalog.Products;

public record ProductState : State<ProductState>
{
    public ProductId Id { get; init; } = null!;
    public ProductStatus Status { get; init; } = ProductStatus.Unset;
    public Name Name { get; init; } = null!;
    public Sku Sku { get; init; } = null!;
    public Description Description { get; init; } = null!;

    public ProductState()
    {
        On<V1.ProductInitialized>(HandleInitialized);
        On<V1.ProductDescriptionDrafted>(HandleDescriptionDrafted);
        On<V1.ProductConfirmed>(HandleConfirmed);
        On<V1.ProductDeprecated>(HandleDeprecated);
        On<V1.ProductCancelled>(HandleCancelled);
    }

    private static ProductState HandleInitialized(
        ProductState state,
        V1.ProductInitialized @event)
        => state with
    {
        Id = new ProductId(@event.ProductId),
        Status = ProductStatus.Initialized,
        Name = new Name(@event.Name),
        Sku = new Sku(@event.Sku)
    };

    private static ProductState HandleDescriptionDrafted(
        ProductState state,
        V1.ProductDescriptionDrafted @event)
        => state with
        {
            Description = new Description(@event.Description)
        };

    private static ProductState HandleConfirmed(
        ProductState state,
        V1.ProductConfirmed @event)
    {
        if (state.Status != ProductStatus.Initialized)
            throw new DomainException("Product must be initialized before confirmation");

        if (state.Description is not null)
            throw new DomainException("Product must have a non-empty description before confirmation");

        return state with { Status = ProductStatus.Confirmed };
    }

    private static ProductState HandleDeprecated(
        ProductState state,
        V1.ProductDeprecated @event)
    {
        if (state.Status != ProductStatus.Confirmed)
            throw new DomainException("Product can only be deprecated after before confirmation");

        return state with { Status = ProductStatus.Deprecated };
    }

    private static ProductState HandleCancelled(
        ProductState state,
        V1.ProductCancelled @event)
    {
        if (state.Status == ProductStatus.Confirmed)
            throw new DomainException("Product cannot be cancelled after confirmation, only deprecated");

        return state with { Status = ProductStatus.Cancelled };
    }
}
