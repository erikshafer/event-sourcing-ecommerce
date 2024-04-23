using Eventuous;

namespace Inventory.Inventories;

public sealed record InventoryId(string Value) : Id(Value);
