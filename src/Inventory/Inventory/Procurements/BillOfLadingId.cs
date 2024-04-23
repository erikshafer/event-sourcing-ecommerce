using Eventuous;

namespace Inventory.Procurements;

public sealed record BillOfLadingId(string Value) : Id(Value);
