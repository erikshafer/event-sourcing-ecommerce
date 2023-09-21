using Wolverine.Attributes;

namespace Catalog.Inventories;

public record RestockThresholdEnabled(Guid InventoryId, int RestockThreshold);

public record DefineRestockThreshold(Guid InventoryId, Quantity RestockThreshold);

[WolverineHandler]
public class DefineRestockThresholdHandler
{
    // TODO
}
