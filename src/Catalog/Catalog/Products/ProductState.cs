using Eventuous;

using static Catalog.Products.ProductEvents;

namespace Catalog.Products;

public record ProductState : State<ProductState>
{
    public ProductId Id { get; init; } = null!;
    public Creation Creation { get; init; } = null!;
    public ProductStatus Status { get; init; } = ProductStatus.Unset;
    public Name Name { get; init; } = null!;
    public Sku Sku { get; init; } = null!;
    public Description Description { get; init; } = null!;

    public ProductState()
    {
        On<V1.ProductDrafted>(Handle);
        On<V1.ProductDescriptionAdjusted>(Handle);
        On<V1.ProductActivated>(Handle);
        On<V1.ProductArchived>(Handle);
        On<V1.ProductDraftCancelled>(Handle);
        On<V1.ProductNameAdjusted>(Handle);
    }

    private static ProductState Handle(
        ProductState state,
        V1.ProductDrafted @event)
        => state with
    {
        Id = new ProductId(@event.ProductId),
        Creation = new Creation(@event.CreatedAt, @event.CreatedBy),
        Status = ProductStatus.Drafted,
        Name = new Name(@event.Name),
        Description = new Description(@event.Description),
        Sku = new Sku(@event.Sku)
    };

    private static ProductState Handle(ProductState state, V1.ProductActivated @event)
    {
        if (state.Status != ProductStatus.Drafted)
            throw new DomainException($"Product must be be {nameof(ProductStatus.Drafted)} status to be activated");

        return state with { Status = ProductStatus.Activated };
    }

    private static ProductState Handle(ProductState state, V1.ProductArchived @event)
    {
        if (state.Status != ProductStatus.Activated)
            throw new DomainException($"Product can only be set to {nameof(ProductStatus.Archived)} while in {nameof(ProductStatus.Activated)}");

        return state with { Status = ProductStatus.Archived };
    }

    private static ProductState Handle(ProductState state, V1.ProductDraftCancelled @event)
    {
        if (state.Status != ProductStatus.Drafted)
            throw new DomainException($"Product can only be set to {nameof(ProductStatus.Cancelled)} from {nameof(ProductStatus.Drafted)}");

        return state with { Status = ProductStatus.Cancelled };
    }

    private static ProductState Handle(ProductState state, V1.ProductDescriptionAdjusted @event)
    {
        if (state.Status is not ProductStatus.Drafted or ProductStatus.Activated)
            throw new DomainException($"Product must be set to {nameof(ProductStatus.Drafted)} or {nameof(ProductStatus.Activated)} to adjust {nameof(Description)}");

        return state with
        {
            Description = new Description(@event.Description)
        };
    }

    private static ProductState Handle(ProductState state, V1.ProductNameAdjusted @event)
    {
        if (state.Status is not ProductStatus.Drafted or ProductStatus.Activated)
            throw new DomainException($"Product must be set to {nameof(ProductStatus.Drafted)} or {nameof(ProductStatus.Activated)} to adjust {nameof(Name)}");

        var adjustedName = new Name(@event.Name);

        if (state.Name.HasSameValue(adjustedName))
            throw new DomainException("Product name is the same");

        return state with { Name = adjustedName };
    }
}
