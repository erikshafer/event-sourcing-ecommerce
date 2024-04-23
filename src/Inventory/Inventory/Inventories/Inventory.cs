using Eventuous;

using static Inventory.Inventories.InventoryEvents;

namespace Inventory.Inventories;

public class Inventory : Aggregate<InventoryState>
{
    public async Task Initialize(
        string inventoryId,
        string sku,
        Services.IsInventoryAvailableBySku isInventoryAvailableBySku)
    {
        EnsureDoesntExist();
        await EnsureSkuAvailable(new Sku(sku), isInventoryAvailableBySku);

        Apply(new V1.InventoryInitialized(
            inventoryId,
            sku));
    }

    public void StockingFromProcurementOrder(
        string inventoryId,
        string procurementOrderId,
        int quantityStocked,
        DateTimeOffset stockedAt)
    {
        EnsureExists();

        Apply(new V1.InventoryStockedFromProcurementOrder(
            inventoryId,
            procurementOrderId,
            quantityStocked
        ));
    }

    private static async Task EnsureSkuAvailable(
        Sku sku,
        Services.IsInventoryAvailableBySku isInventoryAvailableBySku)
    {
        var skuAvailable = await isInventoryAvailableBySku(sku);
        if (!skuAvailable)
            throw new DomainException("SKU not available");
    }
}