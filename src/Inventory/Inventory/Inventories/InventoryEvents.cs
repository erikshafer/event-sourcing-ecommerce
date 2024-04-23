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
            int QuantityStocked
        );

        [EventType("V1.InventoryIncremented")]
        public record InventoryIncremented(
            string InventoryId,
            int QuantityIncremented
        );

        [EventType("V1.InventoryDecremented")]
        public record InventoryDecremented(
            string InventoryId,
            int QuantityDecremented
        );

        [EventType("V1.InventoryHeldInCheckout")]
        public record InventoryHeldInCheckout(
            string InventoryId,
            string CheckoutId,
            int QuantityHeld
        );

        [EventType("V1.InventoryHoldExpiredWithoutOrder")]
        public record InventoryHoldExpiredWithoutOrder(
            string InventoryId,
            string CheckoutId
        );

        [EventType("V1.InventoryHoldLiftedWithOrder")]
        public record InventoryHoldLiftedWithOrder(
            string InventoryId,
            string CheckoutId,
            string OrderId
        );

        [EventType("V1.InventoryPhysicallyCounted")]
        public record InventoryPhysicallyCounted(
            string InventoryId,
            int QuantityCounted
        );

        [EventType("V1.InventoryReorderPointAdjusted")]
        public record InventoryReorderPointAdjusted(
            string InventoryId,
            int ReorderPoint
        );
    }
}
