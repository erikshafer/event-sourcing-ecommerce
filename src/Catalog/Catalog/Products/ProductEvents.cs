using Eventuous;

namespace Catalog.Products;

public static class ProductEvents
{
    public static class V1
    {
        [EventType("V1.ProductDrafted")]
        public record ProductDrafted(
            string ProductId,
            string Sku,
            string Name,
            string Description,
            string Brand,
            DateTimeOffset CreatedAt,
            string CreatedBy
        );

        [EventType("V1.ProductActivated")]
        public record ProductActivated(
            string ProductId,
            DateTimeOffset ActivatedAt,
            string ActivatedBy);

        [EventType("V1.ProductArchived")]
        public record ProductArchived(
            string ProductId,
            DateTimeOffset ArchivedAt,
            string ArchivedBy,
            string Reason
        );

        [EventType("V1.ProductDraftCancelled")]
        public record ProductDraftCancelled(
            string ProductId,
            DateTimeOffset CancelledAt,
            string CancelledBy,
            string Reason
        );

        [EventType("V1.ProductNameAdjusted")]
        public record ProductNameAdjusted(
            string ProductId,
            string Name,
            DateTimeOffset AdjustedAt,
            string AdjustedBy
        );

        [EventType("V1.ProductDescriptionAdjusted")]
        public record ProductDescriptionAdjusted(
            string ProductId,
            string Description,
            DateTimeOffset AdjustedAt,
            string AdjustedBy
        );

        [EventType("V1.ProductBrandAdjusted")]
        public record ProductBrandAdjusted(
            string ProductId,
            string Brand,
            DateTimeOffset AdjustedAt,
            string AdjustedBy
        );
    }
}
