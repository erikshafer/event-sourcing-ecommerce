using Eventuous.Projections.MongoDB.Tools;

namespace Catalog.Api.Queries.Offers;

public record OfferDocument : ProjectedDocument
{
    public OfferDocument(string Id) : base(Id)
    {
    }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.MinValue;
    public string CreatedBy { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public string Status { get; set; } = null!;
}
