namespace Catalog.Inventories;

public record InventorySkuVerified(Guid Id, Guid ProductId, string Sku); // record all the identities for posterity

public record VerifySkuOfInventory(Guid Id, Sku Sku); // the aggregate will have the ProductId

public class VerifySkuOfInventoryHandler
{
    // TODO
}
