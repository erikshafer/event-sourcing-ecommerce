using Eventuous;

namespace Inventory.Orders;

// Orders belong to the Ordering domain.
// This is just a tribute.
public sealed record OrderId(string Value) : Id(Value);
