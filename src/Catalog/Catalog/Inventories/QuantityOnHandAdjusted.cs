using Wolverine.Attributes;

namespace Catalog.Inventories;

public record QuantityOnHandAdjusted(Guid Id, int Quantity, int QuantityDelta, InventoryAdjustmentReason Reason);

public record AdjustQuantityOnHand(Guid Id, Quantity Quantity);

[WolverineHandler]
public class AdjustQuantityOnHandHandler
{
    // TODO
}
