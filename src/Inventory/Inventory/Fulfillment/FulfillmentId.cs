using Eventuous;

namespace Inventory.Fulfillment;

public sealed record FulfillmentId(string Value) : Id(Value);
