using Ecommerce.Eventuous.Exceptions;
using Eventuous;

namespace Catalog.Prices;

public record PriceState : State<PriceState>
{
    public PriceId Id { get; init; } = null!;
    public Creation Creation { get; init; } = null!;
    public Sku Sku { get; init; } = null!;
    public PriceStatus Status { get; init; } = PriceStatus.Unset;
    public Money MinimumAdvertisedPrice { get; init; } = null!;
    public Money ManufacturerSuggestedRetailPrice { get; init; } = null!;
    public BundledPricing BundledPricing { get; init; } = null!;

    public PriceState()
    {
        On<PriceEvents.V1.PriceInitialized>(Handle);
        On<PriceEvents.V1.PriceActivated>(Handle);
        On<PriceEvents.V1.PriceDeprecated>(Handle);
    }

    private static PriceState Handle(
        PriceState state,
        PriceEvents.V1.PriceInitialized @event)
        => state with
        {
            Id = new PriceId(@event.PriceId),
            Sku = new Sku(@event.Sku),
            Creation = new Creation(@event.CreatedAt, @event.CreatedBy),
            Status = PriceStatus.Initialized,
            MinimumAdvertisedPrice = new Money(@event.MinimumAdvertisedPrice, @event.Currency),
            ManufacturerSuggestedRetailPrice = new Money(@event.ManufacturerSuggestedRetailPrice, @event.Currency),
            BundledPricing = new BundledPricing(@event.BundledQuantity, new Money(@event.BundledPrice, @event.Currency)),
        };

    private static PriceState Handle(
        PriceState state,
        PriceEvents.V1.PriceActivated @event)
        => state.Status switch
        {
            // TODO: add validation logic

            PriceStatus.Deprecated => throw InvalidStateChangeException.For<Price, PriceEvents.V1.PriceActivated>(state.Id, PriceStatus.Deprecated),
            _ => state with { Status = PriceStatus.Activated }
        };

    private static PriceState Handle(
        PriceState state,
        PriceEvents.V1.PriceDeprecated @event)
        => state.Status switch
        {
            // TODO: add validation logic

            PriceStatus.Deprecated => throw InvalidStateChangeException.For<Price, PriceEvents.V1.PriceDeprecated>(state.Id, PriceStatus.Deprecated),
            _ => state with { Status = PriceStatus.Deprecated }
        };
}
