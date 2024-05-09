using Eventuous.Projections.MongoDB.Tools;

namespace Catalog.Api.Queries;

public record ProductDocument : ProjectedDocument
{
    public ProductDocument(string Id) : base(Id)
    {
    }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.MinValue;
    public string CreatedBy { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public string Description { get; set; } = null!;
}
