using Eventuous;

namespace Inventory.Procurement;

public sealed record ProcurementId(string Value) : Id(Value);
