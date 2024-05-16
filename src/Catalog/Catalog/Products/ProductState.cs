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

    private static ProductState Handle(ProductState state, V1.ProductActivated @event) => state.Status switch
    {
        ProductStatus.Archived => throw InvalidStateChangeException.For<Product, V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Archived),
        ProductStatus.Cancelled => throw InvalidStateChangeException.For<Product, V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Cancelled),
        _ => state with { Status = ProductStatus.Activated }
    };

    private static ProductState Handle(ProductState state, V1.ProductArchived @event) => state.Status switch
    {
        ProductStatus.Archived => throw InvalidStateChangeException.For<Product, V1.ProductArchived>(state.Id, ProductStatus.Archived),
        ProductStatus.Cancelled => throw InvalidStateChangeException.For<Product, V1.ProductArchived>(state.Id, ProductStatus.Cancelled),
        _ => state with { Status = ProductStatus.Archived }
    };

    private static ProductState Handle(ProductState state, V1.ProductDraftCancelled @event)
    {
        ////////// Thoughts regarding modeling state //////////
        // Does the business really need Archived and Cancelled?
        // Can we model things to be more reflective of how the
        // business actually handles, models, or mentally models
        // these operations?
        // What if we split this single model (AKA entity, aggregate,
        // or stream) into two?
        // Where there's a process for submitting the initial draft
        // and any changes (rough drafts), and then one that has been
        // approved and is now "live" or "active"?

        if (state.Status is not ProductStatus.Drafted)
            throw InvalidStateChangeException.For<Product, V1.ProductDraftCancelled>(state.Id, ProductStatus.Drafted);

        return state with { Status = ProductStatus.Cancelled };
    }

    private static ProductState Handle(ProductState state, V1.ProductDescriptionAdjusted @event)
    {
        // Validation of the value is performed early in the process before overall state is checked.
        var adjustedDescription = new Description(@event.Description);

        // a switch expression
        return state.Status switch
        {
            ProductStatus.Unset => throw InvalidStateChangeException.For<Product, V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Unset),
            ProductStatus.Archived => throw InvalidStateChangeException.For<Product, V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Archived),
            ProductStatus.Cancelled => throw InvalidStateChangeException.For<Product, V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Cancelled),
            _ => state with { Description = adjustedDescription }
        };
    }

    private static ProductState Handle(ProductState state, V1.ProductNameAdjusted @event)
    {
        // Validation of the value is performed early in the process before overall state is checked.
        var adjustedName = new Name(@event.Name);

        // a switch statement
        switch (state.Status)
        {
            case ProductStatus.Unset:
                throw InvalidStateChangeException.For<Product, V1.ProductNameAdjusted>(state.Id, ProductStatus.Unset);
            case ProductStatus.Archived:
                throw InvalidStateChangeException.For<Product, V1.ProductNameAdjusted>(state.Id, ProductStatus.Archived);
            case ProductStatus.Cancelled:
                throw InvalidStateChangeException.For<Product, V1.ProductNameAdjusted>(state.Id, ProductStatus.Cancelled);
            case ProductStatus.Drafted:
            case ProductStatus.Activated:
            case ProductStatus.Closed:
            default:
                break;
        }

        ////////// Thoughts regarding event store immutability //////////
        // Is this exception okay? It should be. However, when aggregating the
        // state (events) in the future, and if the event stream was illegally
        // modified (as it should be immutable), then there could be issues.
        // This is why it's important an event store's log truly be immutable,
        // as business logic / use cases are built around expecting it to be so.
        // An event store is the source of truth. An audit log. A ledger of transactions.

        if (state.Name.HasSameValue(adjustedName))
            throw InvalidStateChangeException.For<Product, V1.ProductNameAdjusted>(state.Id, "Incoming name value is the same as current name");

        return state with { Name = adjustedName };
    }
}
