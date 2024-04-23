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

        Apply(new V1.Initialized(
            inventoryId,
            sku));
    }

    public void StockByProcurementOrder(
        string inventoryId,
        string procurementOrderId,
        int quantityStocked,
        DateTimeOffset stockedAt)
    {
        EnsureExists();

        Apply(new V1.StockedByProcurementOrder(
            inventoryId,
            procurementOrderId,
            quantityStocked,
            stockedAt));
    }

    public void AdjustReorderPoint(
        string inventoryId,
        int reorderPoint)
    {
        EnsureExists();

        Apply(new V1.ReorderPointAdjusted(
            inventoryId,
            reorderPoint));
    }

    public void CountPhysicalInventory(
        string inventoryId,
        int quantityCounted,
        string countedBy,
        DateTimeOffset countedAt)
    {
        EnsureExists();

        Apply(new V1.PhysicalCounted(
            inventoryId,
            quantityCounted,
            countedBy,
            countedAt));
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
