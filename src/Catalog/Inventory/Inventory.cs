using Ecommerce.Core.Aggregates;

namespace Inventory;

public sealed class Inventory : Aggregate
{
    public Guid ProductId { get; private set; }

    public int QuantityOnHand { get; private set; }

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
        QuantityOnHand = 0;
    }
}
