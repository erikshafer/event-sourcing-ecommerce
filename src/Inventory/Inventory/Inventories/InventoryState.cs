using Eventuous;

namespace Inventory.Inventories;

public record InventoryState : State<InventoryState>
{
    public InventoryId Id { get; init; } = null!;
    public InventoryStatus Status { get; init; } = InventoryStatus.Unset;
    public Sku Sku { get; init; } = null!;

    public InventoryState()
    {
        On<InventoryEvents.V1.InventoryInitialized>(HandleInitialized);
    }

    private static InventoryState HandleInitialized(
        InventoryState state,
        InventoryEvents.V1.InventoryInitialized @event) =>
        state with
        {
            Id = new InventoryId(@event.InventoryId),
            Status = InventoryStatus.Initialized,
            Sku = new Sku(@event.Sku)
        };
}
