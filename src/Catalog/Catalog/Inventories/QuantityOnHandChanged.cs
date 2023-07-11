namespace Catalog.Inventories;

public record QuantityOnHandChanged(Guid Id, int Quantity, int QuantityDelta);

public record ChangeQuantityOnHand(Guid Id, Quantity Quantity);
