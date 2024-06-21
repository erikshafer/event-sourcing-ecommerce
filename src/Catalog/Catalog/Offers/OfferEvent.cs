using Eventuous;

namespace Catalog.Offers;

public abstract record OfferEvent
{
    public static class V1
    {
        [EventType("V1.OfferDrafted")]
        public record OfferDrafted(
            string OfferId,
            string Sku,
            DateTimeOffset CreatedAt,
            string CreatedBy
        ) : OfferEvent;

        [EventType("V1.OfferActivated")]
        public record OfferActivated(
            string OfferId,
            DateTimeOffset ActivatedAt,
            string ActivatedBy
        ) : OfferEvent;
    }
}
