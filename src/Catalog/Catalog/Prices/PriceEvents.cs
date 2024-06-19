using Eventuous;

namespace Catalog.Prices;

public static class PriceEvents
{
    public static class V1
    {
        [EventType("V1.PriceInitialized")]
        public record PriceInitialized(
            string PriceId,
            string Sku,
            decimal MinimumAdvertisedPrice,
            decimal ManufacturerSuggestedRetailPrice,
            int BundledQuantity,
            decimal BundledPrice,
            string Currency,
            DateTimeOffset CreatedAt,
            string CreatedBy
        );

        [EventType("V1.PriceActivated")]
        public record PriceActivated(
            string PriceId,
            DateTimeOffset ActivatedAt,
            string ActivatedBy
        );

        [EventType("V1.PriceDeprecated")]
        public record PriceDeprecated(
            string ProductId,
            DateTimeOffset CancelledAt,
            string CancelledBy,
            string Reason
        );
    }
}
