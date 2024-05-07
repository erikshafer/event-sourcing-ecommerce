using Eventuous;

namespace Catalog.Products;

public static class ProductEvents
{
    public static class V1
    {
        [EventType("V1.ProductInitialized")]
        public record ProductInitialized(
            string ProductId,
            string Sku,
            string Name,
            string ShortDescription,
            string LongDescription
        );

        [EventType("V1.ProductConfirmed")]
        public record ProductConfirmed(
            string ProductId,
            string ConfirmedBy,
            DateTimeOffset ConfirmedAt);

        [EventType("V1.ProductDeprecated")]
        public record ProductDeprecated(
            string ProductId,
            string DeprecatedBy,
            string Reason
        );

        [EventType("V1.ProductCancelled")]
        public record ProductCancelled(
            string ProductId,
            string CancelledBy,
            string Reason
        );
    }
}
