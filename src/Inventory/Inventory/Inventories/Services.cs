namespace Inventory.Inventories;

public static class Services
{
    public delegate ValueTask<bool> IsInventorySkuAvailable(Sku sku);
}
