namespace Catalog.Inventories;

public record InventoryInitialized(Guid InventoryId, Guid ProductId);

public record InitializeInventory(Guid InventoryId, Guid ProductId);

public class InitializeInventoryHandler
{
    // TODO
}
