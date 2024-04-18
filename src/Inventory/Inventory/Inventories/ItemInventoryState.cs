using Eventuous;

namespace Inventory.Inventories;

public record ItemInventoryState : State<ItemInventoryState>
{
    public InventoryId Id { get; init; } = null!;
    public ItemInventoryStatus Status { get; init; } = ItemInventoryStatus.Unset;
    public Sku Sku { get; init; } = null!;

    public ItemInventoryState()
    {
        On<InventoryEvents.V1.InventoryInitialized>(HandleInitialized);
    }

    private static ItemInventoryState HandleInitialized(
        ItemInventoryState state,
        InventoryEvents.V1.InventoryInitialized @event) =>
        state with
        {
            Id = new InventoryId(@event.InventoryId),
            Status = ItemInventoryStatus.Initialized,
            Sku = new Sku(@event.Sku)
        };
}
