namespace Inventory.Inventories;

public static class InventoryCommands
{
    public static class V1
    {
        public record InitializeInventory(
            string Sku
        );

        public record StockInventoryFromProcurementOrder(
            string InventoryId,
            string ProcurementId,
            int Quantity
        );

        public record IncrementInventory(
            string InventoryId,
            int Quantity
        );

        public record DecrementInventory(
            string InventoryId,
            int Quantity
        );
    }
}
