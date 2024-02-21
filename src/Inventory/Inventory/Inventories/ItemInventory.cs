using Eventuous;

using static Inventory.Inventories.InventoryEvents;

namespace Inventory.Inventories;

public class ItemInventory : Aggregate<ItemInventoryState>
{
    public async Task InitializeInventory(
        string inventoryId,
        string sku,
        Services.IsInventorySkuAvailable isInventorySkuAvailable)
    {
        EnsureDoesntExist();
        await EnsureSkuAvailable(new Sku(sku), isInventorySkuAvailable);

        Apply(new V1.InventoryInitialized(inventoryId, sku));
    }

    private static async Task EnsureSkuAvailable(Sku sku, Services.IsInventorySkuAvailable isInventorySkuAvailable)
    {
        var skuAvailable = await isInventorySkuAvailable(sku);
        if (!skuAvailable)
            throw new DomainException("SKU not available");
    }
}
