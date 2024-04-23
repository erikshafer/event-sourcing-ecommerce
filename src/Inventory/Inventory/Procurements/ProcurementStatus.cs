namespace Inventory.Procurements;

public enum ProcurementStatus
{
    Unset = 0,
    Initialized = 1,
    OrderMade = 2,
    OrderReceived = 4,
    Cancelled = 8
}
