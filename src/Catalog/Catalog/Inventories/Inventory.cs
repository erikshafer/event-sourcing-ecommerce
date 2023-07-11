using Catalog.Inventories.SkuValidation;
using Ecommerce.Core.Aggregates;
using Ecommerce.Core.Exceptions;

namespace Catalog.Inventories;

public sealed class Inventory : Aggregate
{
    public Guid ProductId { get; private set; }

    public Sku Sku { get; private set; } = Sku.Empty();

    public InventoryStatus Status { get; private set; }

    public Quantity QuantityOnHand { get; private set; } = Quantity.Zero();

    public Quantity RestockThreshold { get; private set; } = Quantity.Zero();

    public Inventory()
    {
    }

    public Inventory(InventoryInitialized @event)
    {
        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(InventoryInitialized @event)
    {
        Id = @event.InventoryId;
        ProductId = @event.ProductId;
        Status = InventoryStatus.Initialized;
    }

    public void VerifySku(ISkuValidator validator, Sku sku)
    {
        if (Status != InventoryStatus.Initialized)
            throw new InvalidOperationException($"Verifying inventory cannot be done in '{Status}' status");

        var activeProductIdFromSku = validator.Validate(sku);

        if (activeProductIdFromSku == Guid.Empty)
            throw new InvalidOperationException($"Verifying inventory encountered empty product identity");

        if (ProductId.Equals(activeProductIdFromSku) is false)
            throw InvalidAggregateOperationException.For<Inventory>(Id, nameof(VerifySku));

        var @event = new InventorySkuVerified(Id, ProductId, sku.Value);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(InventorySkuVerified @event)
    {
        Sku = new Sku(@event.Sku);
        Status = InventoryStatus.SkuVerified;
    }

    public void Adjust(Quantity quantity, InventoryAdjustmentReason reason)
    {
        if (Status is not (InventoryStatus.SkuVerified or InventoryStatus.Live))
            throw new InvalidOperationException($"Verifying inventory cannot be done in '{Status}' status");

        if (reason is InventoryAdjustmentReason.Unset)
            throw new InvalidOperationException($"Must have defined reason for inventory adjustment");

        var delta = QuantityOnHand.CalculateDelta(quantity);

        if (delta.IsZero())
            throw new InvalidOperationException("No change in quantity on hand"); // discuss

        var @event = new QuantityOnHandAdjusted(Id, quantity.Value, delta.Value, reason);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(QuantityOnHandAdjusted @event)
    {
        QuantityOnHand = new Quantity(@event.Quantity);
    }

    public void MarkAsLive()
    {
        if (Status is not InventoryStatus.SkuVerified)
            throw new InvalidOperationException($"Marking inventory as live from '{Status}' status is not allowed");

        // TODO: discuss
    }

    public void DefineRestockThreshold(Quantity restockThreshold)
    {
        if (restockThreshold.IsLessThanZero())
            throw new InvalidOperationException($"Restock threshold cannot be '{restockThreshold.Value}', must be zero or greater");

        var @event = new RestockThresholdEnabled(Id, restockThreshold.Value);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(RestockThresholdEnabled @event)
    {
        RestockThreshold = new Quantity(@event.RestockThreshold);
    }
}
