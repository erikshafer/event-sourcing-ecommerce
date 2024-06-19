using Ecommerce.Core.Extensions;
using Ecommerce.Eventuous.Exceptions;
using Eventuous;

namespace Catalog.Products;

public record ProductState : State<ProductState>
{
    public ProductId Id { get; init; } = null!;
    public Creation Creation { get; init; } = null!;
    public ProductStatus Status { get; init; } = ProductStatus.Unset;
    public Name Name { get; init; } = null!;
    public Sku Sku { get; init; } = null!;
    public Description Description { get; init; } = null!;
    public Brand Brand { get; init; } = null!;
    public IList<Measurement> Measurements { get; init; } = null!;

    public ProductState()
    {
        On<ProductEvents.V1.ProductDrafted>(Handle);
        On<ProductEvents.V1.ProductActivated>(Handle);
        On<ProductEvents.V1.ProductArchived>(Handle);
        On<ProductEvents.V1.ProductDraftCancelled>(Handle);
        On<ProductEvents.V1.ProductDescriptionAdjusted>(Handle);
        On<ProductEvents.V1.ProductNameAdjusted>(Handle);
        On<ProductEvents.V1.ProductBrandAdjusted>(Handle);
        On<ProductEvents.V1.ProductTakeMeasurement>(Handle);
        On<ProductEvents.V1.ProductRemoveMeasurement>(Handle);
    }

    private static ProductState Handle(
        ProductState state,
        ProductEvents.V1.ProductDrafted @event)
        => state with
    {
        Id = new ProductId(@event.ProductId),
        Creation = new Creation(@event.CreatedAt, @event.CreatedBy),
        Status = ProductStatus.Drafted,
        Name = new Name(@event.Name),
        Description = new Description(@event.Description),
        Sku = new Sku(@event.Sku),
        Brand = new Brand(@event.Brand),
        Measurements = []
    };

    private static ProductState Handle(ProductState state, ProductEvents.V1.ProductActivated @event) => state.Status switch
    {
        ProductStatus.Archived => throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductActivated>(state.Id, ProductStatus.Archived),
        ProductStatus.Cancelled => throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductActivated>(state.Id, ProductStatus.Cancelled),
        _ => state with { Status = ProductStatus.Activated }
    };

    private static ProductState Handle(ProductState state, ProductEvents.V1.ProductArchived @event) => state.Status switch
    {
        ProductStatus.Archived => throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductArchived>(state.Id, ProductStatus.Archived),
        ProductStatus.Cancelled => throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductArchived>(state.Id, ProductStatus.Cancelled),
        _ => state with { Status = ProductStatus.Archived }
    };

    private static ProductState Handle(ProductState state, ProductEvents.V1.ProductDraftCancelled @event)
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
            throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductDraftCancelled>(state.Id, ProductStatus.Drafted);

        return state with { Status = ProductStatus.Cancelled };
    }

    private static ProductState Handle(ProductState state, ProductEvents.V1.ProductDescriptionAdjusted @event)
    {
        // Validation of the value is performed early in the process before overall state is checked.
        var adjustedDescription = new Description(@event.Description);

        // a switch expression
        return state.Status switch
        {
            ProductStatus.Unset => throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Unset),
            ProductStatus.Archived => throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Archived),
            ProductStatus.Cancelled => throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductDescriptionAdjusted>(state.Id, ProductStatus.Cancelled),
            _ => state with { Description = adjustedDescription }
        };
    }

    private static ProductState Handle(ProductState state, ProductEvents.V1.ProductNameAdjusted @event)
    {
        // Validation of the value is performed early in the process before overall state is checked.
        var adjustedName = new Name(@event.Name);

        // a switch statement
        switch (state.Status)
        {
            case ProductStatus.Unset:
                throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductNameAdjusted>(state.Id, ProductStatus.Unset);
            case ProductStatus.Archived:
                throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductNameAdjusted>(state.Id, ProductStatus.Archived);
            case ProductStatus.Cancelled:
                throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductNameAdjusted>(state.Id, ProductStatus.Cancelled);
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
            throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductNameAdjusted>(state.Id,
                $"Incoming name value is the same as current name: {state.Name}");

        return state with { Name = adjustedName };
    }

    private static ProductState Handle(ProductState state, ProductEvents.V1.ProductBrandAdjusted @event)
    {
        if (state.Status is ProductStatus.Closed)
            throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductBrandAdjusted>(state.Id, state.Status);

        var adjustedBrand = new Brand(@event.Brand);

        if (state.Brand.HasSameValue(adjustedBrand))
            throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductBrandAdjusted>(state.Id,
                $"Incoming brand value is the same as current brand: {state.Brand}");

        return state with { Brand = adjustedBrand };
    }

    private static ProductState Handle(ProductState state, ProductEvents.V1.ProductTakeMeasurement @event)
    {
        if (state.Status is ProductStatus.Closed)
            throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductTakeMeasurement>(state.Id, state.Status);

        var incoming = new Measurement(@event.Type, @event.Unit, @event.Value);
        var exists = FindMeasurementTypeMatchingWith(state.Measurements, incoming.GetMeasurementType());

        if (exists is not null)
        {
            if (exists.Matches(incoming))
                return state; // no-op if everything matches existing measurements properties (type, unit, value)

            if (exists.MatchesTypeAndUnit(incoming))
            {
                state.Measurements.Replace(exists, incoming);
                return state; // mutated state's Measurements by replacing the existing Measurement with its new value
            }
        }

        // if the measurement type is not in the existing state, add it // TODO: tests
        state.Measurements.Add(incoming);
        return state;
    }

    private static ProductState Handle(ProductState state, ProductEvents.V1.ProductRemoveMeasurement @event)
    {
        if (state.Status is ProductStatus.Closed)
            throw InvalidStateChangeException.For<Product, ProductEvents.V1.ProductRemoveMeasurement>(state.Id, state.Status);

        var typeToRemove = Measurement.GetName(@event.Type);
        var exists = FindMeasurementTypeMatchingWith(state.Measurements, typeToRemove);

        if (exists is null)
            return state; // no-op as the type does not exist (Also, how did we get to this logic branch?)

        var existing = state.Measurements.First(m => m.MatchesType(typeToRemove));
        state.Measurements.Remove(existing);
        return state; // mutated state's Measurements by removing the corresponding Measurement by its type property
    }

    private static Measurement? FindMeasurementMatchingWith(IList<Measurement> measurements, Measurement measurement) =>
        measurements.SingleOrDefault(m => m.Matches(measurement));

    private static Measurement? FindMeasurementTypeMatchingWith(IList<Measurement> measurements, MeasurementType measurementType) =>
        measurements.SingleOrDefault(m => m.MatchesType(measurementType));
}
