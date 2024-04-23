using Eventuous;

namespace Inventory.Inventories;

public static class InventoryEvents
{
    public static class V1
    {
        [EventType("V1.InventoryInitialized")]
        public record Initialized(
            string InventoryId,
            string Sku
        );

        [EventType("V1.ProcurementOrderReceived")]
        public record ProcurementOrderReceived(
            string InventoryId,
            string ProcurementOrderId,
            string ArrivedInShipmentId,
            DateTimeOffset ArrivedAt
        );

        [EventType("V1.ProcurementOrderVerified")]
        public record ProcurementOrderVerified(
            string InventoryId,
            string ProcurementOrderId,
            bool QuantityIsAccurate,
            int ActualQuantity,
            string VerifiedBy,
            DateTimeOffset VerifiedAt
        );

        [EventType("V1.ProcurementInventoryStocked")]
        public record ProcurementInventoryStocked(
            string InventoryId,
            string ProcurementOrderId,
            int QuantityStocked,
            string StockedBy,
            DateTimeOffset StockedAt
        );

        [EventType("V1.InventoryDeemedUsable")]
        public record InventoryDeemedUsable(
            string InventoryId,
            string DeemedUsableBy,
            DateTimeOffset DeemedUsableAt
        );

        [EventType("V1.InventoryStockedByProcurementOrder")]
        public record StockedByProcurementOrder(
            string InventoryId,
            string ProcurementOrderId,
            int QuantityStocked,
            DateTimeOffset StockedAt
        );

        [EventType("V1.InventoryHeldInCheckout")]
        public record HeldInCheckout(
            string InventoryId,
            string CheckoutId,
            int QuantityHeld,
            int HoldingDurationInSeconds,
            DateTimeOffset HeldAt,
            DateTimeOffset ExpiresAt
        );

        [EventType("V1.InventoryHoldExpiredWithoutOrder")]
        public record HoldExpiredWithoutOrder(
            string InventoryId,
            string CheckoutId,
            DateTimeOffset HoldExpiredAt
        );

        [EventType("V1.InventoryHoldExpiredWithoutOrder")]
        public record HoldLiftedWithOrder(
            string InventoryId,
            string CheckoutId,
            DateTimeOffset HoldLiftedAt
        );

        [EventType("V1.PhysicalCounted")]
        public record PhysicalCounted(
            string InventoryId,
            int QuantityCounted,
            string CountedBy,
            DateTimeOffset CountedAt
        );

        [EventType("V1.InventoryReorderPointAdjusted")]
        public record ReorderPointAdjusted(
            string InventoryId,
            int ReorderPoint
        );
    }
}
