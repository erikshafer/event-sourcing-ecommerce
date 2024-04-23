using Eventuous;

namespace Inventory.Fulfillment;

public record FulfillmentState : State<FulfillmentState>
{
    public FulfillmentId Id { get; init; } = null!;
}
