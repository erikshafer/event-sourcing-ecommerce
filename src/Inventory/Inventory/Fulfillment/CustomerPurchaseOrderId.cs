using Eventuous;

namespace Inventory.Fulfillment;

// Customer-facing orders belong to the Ordering domain.
// This is just a tribute.
public sealed record CustomerPurchaseOrderId(string Value) : Id(Value);
