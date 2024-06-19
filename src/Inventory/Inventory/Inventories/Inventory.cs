using Eventuous;

using static Inventory.Inventories.InventoryEvents;

namespace Inventory.Inventories;

public class Inventory : Aggregate<InventoryState>
{
    private static async Task EnsureSkuAvailable(
        Sku sku,
        Services.IsInventoryAvailableBySku isInventoryAvailableBySku)
    {
        var skuAvailable = await isInventoryAvailableBySku(sku);
        if (!skuAvailable)
            throw new DomainException("SKU not available");
    }
}
