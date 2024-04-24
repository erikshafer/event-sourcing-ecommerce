namespace Inventory.Fulfillment;

public static class Services
{
    public delegate ValueTask<bool> IsCustomerPurchaseOrderComplete(CustomerPurchaseOrderId id);
}
