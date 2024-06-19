using Eventuous;

namespace Inventory.Inventories;

using Events = InventoryEvents.V1;

public record InventoryState : State<InventoryState>
{
    public InventoryId Id { get; init; } = null!;
    public InventoryStatus Status { get; init; } = InventoryStatus.Unset;
    public Sku Sku { get; init; } = null!;
    public Quantity Quantity { get; init; } = null!;

    public InventoryState()
    {
        On<Events.InventoryInitialized>(Handle);
        On<Events.InventoryStockedFromProcurementOrder>(Handle);
    }

    private static InventoryState Handle(
        InventoryState state,
        Events.InventoryInitialized @event) =>
        state with
        {
            Id = new InventoryId(@event.InventoryId),
            Status = InventoryStatus.Initialized,
            Sku = new Sku(@event.Sku),
            Quantity = Quantity.IsZero,
        };

    private static InventoryState Handle(
        InventoryState state,
        Events.InventoryStockedFromProcurementOrder @event) =>
        state with
        {
            Quantity = state.Quantity.Add(@event.Quantity)
        };
}
