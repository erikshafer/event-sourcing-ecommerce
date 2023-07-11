namespace Catalog.Inventories;

public enum InventoryAdjustmentReason
{
    Unset = 0,
    Unknown = 1,
    RecountCorrection = 2,
    CustomerOrderConfirmed = 3,
    CustomerOrderReturnedUnopened = 4,
    ProcurementOrderReceived = 5,
    ProcurementOrderReturned = 6,
    DamagedInFulfillment = 7,
}
