using Eventuous;

namespace Inventory.Fulfillment;

public static class FulfillmentEvents
{
    public static class V1
    {
        [EventType("V1.FulfillmentOrderPlaced")]
        public record FulfillmentOrderPlaced(
            string FulfillmentId,
            string OrderId
        );
    }
}
