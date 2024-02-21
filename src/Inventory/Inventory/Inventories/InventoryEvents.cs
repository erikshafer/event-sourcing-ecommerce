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
    }
}
