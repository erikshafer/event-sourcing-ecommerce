using Eventuous.Projections.MongoDB.Tools;

namespace Catalog.Api.Queries;

public record ProductDocument : ProjectedDocument
{
    public ProductDocument(string Id) : base(Id)
    {
    }

    public string Status { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string Sku { get; init; } = null!;
}
