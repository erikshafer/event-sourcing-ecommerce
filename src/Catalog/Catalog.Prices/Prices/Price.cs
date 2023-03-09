using Ecommerce.Core.Aggregates;

namespace Catalog.Prices.Prices;

public sealed class Price : Aggregate
{
    public Guid ProductId { get; private set; }

    public decimal MinimumAdvertisedPrice { get; private set; }
    public decimal Map() => MinimumAdvertisedPrice;

    public decimal ManufacturersSuggestedRetailPrice { get; private set; }
    public decimal Msrp() => ManufacturersSuggestedRetailPrice;

    public decimal BaselinePrice { get; private set; }

    public Price()
    {
    }

    public Price(PriceDrafted @event)
    {
        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(PriceDrafted @event)
    {
        Id = @event.PriceId;
        ProductId = @event.ProductId;
    }
}
