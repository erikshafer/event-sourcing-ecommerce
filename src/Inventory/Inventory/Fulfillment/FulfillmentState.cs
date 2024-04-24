using Eventuous;

namespace Inventory.Fulfillment;

using Events = FulfillmentEvents.V1;

public record FulfillmentState : State<FulfillmentState>
{
    public FulfillmentId Id { get; init; } = null!;
    public FulfillmentStatus Status { get; init; } = FulfillmentStatus.Unset;
    public CustomerPurchaseOrderId CustomerPurchaseOrderId { get; init; } = null!;
    public Sku Sku { get; init; } = null!;
    public Quantity Quantity { get; init; } = null!;

    public FulfillmentState()
    {
        On<Events.FulfillmentOrderPlaced>(Handle);
        On<Events.FulfillmentOrderCompleted>(Handle);
    }

    private static FulfillmentState Handle(
        FulfillmentState state,
        Events.FulfillmentOrderPlaced @event) =>
        state with
    {
        Id = new FulfillmentId(@event.FulfillmentId),
        Status = FulfillmentStatus.Initialized,
        CustomerPurchaseOrderId = new CustomerPurchaseOrderId(@event.OrderId),
        Sku = new Sku(@event.Sku),
        Quantity = new Quantity(@event.Quantity)
    };

    private static FulfillmentState Handle(
        FulfillmentState state,
        Events.FulfillmentOrderCompleted @event) =>
        state with
    {
        Status = FulfillmentStatus.Completed
    };
}
