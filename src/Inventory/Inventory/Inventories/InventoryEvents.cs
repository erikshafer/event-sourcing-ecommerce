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

        [EventType("V1.ProcurementOrderArrived")]
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

        [EventType("V1.InventoryHeldInCheckout")]
        public record InventoryHeldInCheckout(
            string InventoryId,
            string CheckoutId,
            int QuantityHeld,
            int HoldingDurationInSeconds,
            DateTimeOffset HeldAt,
            DateTimeOffset ExpiresAt
        );

        [EventType("V1.InventoryHoldExpiredWithoutOrder")]
        public record InventoryHoldExpiredWithoutOrder(
            string InventoryId,
            string CheckoutId,
            DateTimeOffset HoldExpiredAt
        );

        [EventType("V1.InventoryHoldExpiredWithoutOrder")]
        public record InventoryHoldLiftedWithOrder(
            string InventoryId,
            string CheckoutId,
            DateTimeOffset HoldLiftedAt
        );

        [EventType("V1.InventoryCounted")]
        public record InventoryCounted(
            string InventoryId,
            string AmountCounted,
            string CountedBy,
            DateTimeOffset CountedAt
        );
    }
}
