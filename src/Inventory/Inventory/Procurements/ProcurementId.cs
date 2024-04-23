using Eventuous;

namespace Inventory.Procurements;

public sealed record ProcurementId(string Value) : Id(Value);
