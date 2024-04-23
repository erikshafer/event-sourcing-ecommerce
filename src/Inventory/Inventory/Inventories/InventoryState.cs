using Eventuous;

namespace Inventory.Inventories;

public record InventoryState : State<InventoryState>
{
    public InventoryId Id { get; init; } = null!;
    public InventoryStatus Status { get; init; } = InventoryStatus.Unset;
    public Sku Sku { get; init; } = null!;
    public int Quantity { get; init; }

    public InventoryState()
    {
        On<InventoryEvents.V1.InventoryInitialized>(Handle);
        On<InventoryEvents.V1.InventoryStockedFromProcurementOrder>(Handle);
    }

    private static InventoryState Handle(
        InventoryState state,
        InventoryEvents.V1.InventoryInitialized @event) =>
        state with
        {
            Id = new InventoryId(@event.InventoryId),
            Status = InventoryStatus.Initialized,
            Sku = new Sku(@event.Sku),
            Quantity = 0,
        };

    private static InventoryState Handle(
        InventoryState state,
        InventoryEvents.V1.InventoryStockedFromProcurementOrder @event) =>
        state with
        {
            Quantity = state.Quantity + @event.QuantityStocked
        };
}
