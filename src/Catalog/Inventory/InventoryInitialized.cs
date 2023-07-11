using Wolverine.Attributes;

namespace Inventory;

public record InventoryInitialized(Guid InventoryId, Guid ProductId);

public record InitializeInventory(Guid InventoryId, Guid ProductId);

[WolverineHandler]
public class InitializeInventoryHandler
{
    // TODO
}
