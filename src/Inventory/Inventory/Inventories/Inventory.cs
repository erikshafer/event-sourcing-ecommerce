using Eventuous;

using static Inventory.Inventories.InventoryEvents;

namespace Inventory.Inventories;

public class Inventory : Aggregate<InventoryState>
{
    public async Task InitializeInventory(
        string inventoryId,
        string sku,
        Services.IsInventoryAvailableBySku isInventoryAvailableBySku)
    {
        EnsureDoesntExist();
        await EnsureSkuAvailable(new Sku(sku), isInventoryAvailableBySku);

        Apply(new V1.InventoryInitialized(inventoryId, sku));
    }

    private static async Task EnsureSkuAvailable(Sku sku, Services.IsInventoryAvailableBySku isInventoryAvailableBySku)
    {
        var skuAvailable = await isInventoryAvailableBySku(sku);
        if (!skuAvailable)
            throw new DomainException("SKU not available");
    }
}
