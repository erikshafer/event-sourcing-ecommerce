using Eventuous;

namespace Inventory.Inventories;

public static class InventoryEvents
{
    public static class V1
    {
        [EventType("V1.InventoryInitialized")]
        public record InventoryInitialized(
            string InventoryId,
            string Sku
        );

        [EventType("V1.InventoryStockedFromProcurementOrder")]
        public record InventoryStockedFromProcurementOrder(
            string InventoryId,
            string ProcurementId,
            int Quantity
        );

        [EventType("V1.InventoryIncremented")]
        public record InventoryIncremented(
            string InventoryId,
            int Quantity
        );

        [EventType("V1.InventoryDecremented")]
        public record InventoryDecremented(
            string InventoryId,
            int Quantity
        );
    }
}
