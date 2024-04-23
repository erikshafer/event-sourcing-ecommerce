using Eventuous;

namespace Inventory.Inventories;

public record InventoryState : State<InventoryState>
{
    public InventoryId Id { get; init; } = null!;
    public InventoryStatus Status { get; init; } = InventoryStatus.Unset;
    public Sku Sku { get; init; } = null!;
    public int Quantity { get; init; }
    public int ReorderPoint { get; init; } // AKA "ROP", when to order stock

    public InventoryState()
    {
        On<InventoryEvents.V1.Initialized>(Handle);
        On<InventoryEvents.V1.StockedByProcurementOrder>(Handle);
        On<InventoryEvents.V1.ReorderPointAdjusted>(Handle);
        On<InventoryEvents.V1.PhysicalCounted>(Handle);
    }

    private static InventoryState Handle(
        InventoryState state,
        InventoryEvents.V1.Initialized @event) =>
        state with
        {
            Id = new InventoryId(@event.InventoryId),
            Status = InventoryStatus.Initialized,
            Sku = new Sku(@event.Sku),
            Quantity = 0,
            ReorderPoint = 0
        };

    private static InventoryState Handle(
        InventoryState state,
        InventoryEvents.V1.StockedByProcurementOrder @event) =>
        state with
        {
            Quantity = state.Quantity + @event.QuantityStocked
        };

    private static InventoryState Handle(
        InventoryState state,
        InventoryEvents.V1.ReorderPointAdjusted @event) =>
        state with
        {
            ReorderPoint = state.ReorderPoint + @event.ReorderPoint
        };

    private static InventoryState Handle(
        InventoryState state,
        InventoryEvents.V1.PhysicalCounted @event) =>
        state with
        {
            Quantity = state.Quantity + @event.QuantityCounted
        };
}
