using Eventuous;

namespace Inventory.Inventories;

public record InventoryId(string Value) : Id(Value);
