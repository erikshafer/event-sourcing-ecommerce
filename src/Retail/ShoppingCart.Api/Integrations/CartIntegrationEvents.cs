using Eventuous;

namespace ShoppingCart.Api.Integrations;

public static class CartIntegrationEvents {
    [EventType("V1.CartConfirmedRecorded")]
    public record CartConfirmedRecorded(
        string CartId,
        string CustomerId,
        string[] ProductIds,
        decimal TotalPriceQuoted,
        DateTime ConfirmedAt);
}
