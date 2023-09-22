namespace Catalog.Inventories;

public record QuantityOnHandAdjusted(Guid Id, int Quantity, int QuantityDelta, InventoryAdjustmentReason Reason);

public record AdjustQuantityOnHand(Guid Id, Quantity Quantity);

public class AdjustQuantityOnHandHandler
{
    // TODO
}
