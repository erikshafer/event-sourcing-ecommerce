using Eventuous.Projections.MongoDB.Tools;

namespace Catalog.Api.Queries.Prices;

public record PriceDocument : ProjectedDocument
{
    public PriceDocument(string Id) : base(Id)
    {
    }

    public string Sku { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.MinValue;
    public string CreatedBy { get; set; } = null!;
    public string Status { get; set; } = null!;
    public decimal MinimumAdvertisedPrice { get; set; }
    public decimal ManufacturerSuggestedRetailPrice { get; set; }
    // TODO: Bundled Pricing
}
