using Ecommerce.Eventuous.Exceptions;
using Eventuous;

namespace Catalog.Offers;

using Events = OfferEvent.V1;

public record OfferState : State<OfferState>
{
    public OfferId Id { get; init; } = null!;
    public Creation Creation { get; init; } = null!;
    public OfferStatus Status { get; set; } = OfferStatus.Unset;
    public Sku Sku { get; init; } = null!;

    public OfferState()
    {
        On<Events.OfferDrafted>(Handle);
        On<Events.OfferActivated>(Handle);
    }

    private static OfferState Handle(OfferState state, Events.OfferDrafted @event) => state with
    {
        Id = new OfferId(@event.OfferId),
        Creation = new Creation(@event.CreatedAt, @event.CreatedBy),
        Sku = new Sku(@event.Sku)
    };

    private static OfferState Handle(OfferState state, Events.OfferActivated @event) => state.Status switch
    {
        OfferStatus.Cancelled => throw InvalidStateChangeException.For<Offer, Events.OfferActivated>(state.Id, OfferStatus.Cancelled),
        _ => state with { Status = OfferStatus.Activated }
    };
}
