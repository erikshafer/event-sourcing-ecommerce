using Ecommerce.Eventuous.Exceptions;
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
            throw InvalidStateChangeException.For<Product, V1.ProductActivated>(state.Id, ProductStatus.Drafted);

        return state with { Status = ProductStatus.Activated };
    }

    private static ProductState Handle(ProductState state, V1.ProductArchived @event)
    {
        if (state.Status != ProductStatus.Activated)
            throw InvalidStateChangeException.For<Product, V1.ProductArchived>(state.Id, ProductStatus.Activated);

        return state with { Status = ProductStatus.Archived };
    }

    private static ProductState Handle(ProductState state, V1.ProductDraftCancelled @event)
    {
        if (state.Status != ProductStatus.Drafted)
            throw InvalidStateChangeException.For<Product, V1.ProductDraftCancelled>(state.Id, ProductStatus.Drafted);

        return state with { Status = ProductStatus.Cancelled };
    }

    private static ProductState Handle(ProductState state, V1.ProductDescriptionAdjusted @event)
    {
        // TODO this is a scenario that highlights the need for better type checking, handling, errors, etc.
        // multiple valid states -- is this the problem?

        if (state.Status is not ProductStatus.Drafted)
            throw InvalidStateChangeException.For<Product, V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Drafted);

        if (state.Status is not ProductStatus.Activated)
            throw InvalidStateChangeException.For<Product, V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Activated);

        return state with { Description = new Description(@event.Description) };
    }

    private static ProductState Handle(ProductState state, V1.ProductNameAdjusted @event)
    {
        // TODO this is a scenario that highlights the need for better type checking, handling, errors, etc.
        // multiple valid states -- is this the problem?

        if (state.Status is not ProductStatus.Drafted)
            throw InvalidStateChangeException.For<Product, V1.ProductNameAdjusted>(state.Id, ProductStatus.Drafted);

        if (state.Status is not ProductStatus.Activated)
            throw InvalidStateChangeException.For<Product, V1.ProductNameAdjusted>(state.Id, ProductStatus.Activated);

        var adjustedName = new Name(@event.Name);

        if (state.Name.HasSameValue(adjustedName))
            throw new DomainException("Product name is the same");

        return state with { Name = adjustedName };
    }
}
