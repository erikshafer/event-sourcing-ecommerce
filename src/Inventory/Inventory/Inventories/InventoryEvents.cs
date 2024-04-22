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

        [EventType("V1.ProcurementPurchaseOrderArrived")]
        public record ProcurementPurchaseOrderReceived(
            string InventoryId,
            string ProcurementPurchaseOrderId,
            string ArrivedInShipmentId,
            DateTimeOffset ArrivedAt
        );

        [EventType("V1.ProcurementPurchaseOrderVerified")]
        public record ProcurementPurchaseOrderVerified(
            string InventoryId,
            string ProcurementPurchaseOrderId,
            bool QuantityIsAccurate,
            int ActualQuantity,
            string VerifiedBy,
            DateTimeOffset VerifiedAt
        );

        [EventType("V1.ProcurementInventoryStocked")]
        public record ProcurementInventoryStocked(
            string InventoryId,
            string ProcurementPurchaseOrderId,
            int QuantityStocked,
            string StockedBy,
            DateTimeOffset StockedAt
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
    }
}
