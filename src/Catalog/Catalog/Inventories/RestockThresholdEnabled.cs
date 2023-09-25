namespace Catalog.Inventories;

public record RestockThresholdEnabled(Guid InventoryId, int RestockThreshold);

public record DefineRestockThreshold(Guid InventoryId, Quantity RestockThreshold);

public class DefineRestockThresholdHandler
{
    // TODO
}
