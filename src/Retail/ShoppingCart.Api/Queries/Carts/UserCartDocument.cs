using Eventuous.Projections.MongoDB.Tools;

namespace ShoppingCart.Api.Queries.Carts;

public record UserCartDocument : ProjectedDocument
{
    public UserCartDocument(string Id) : base(Id)
    {
    }

    public string CustomerId { get; set; } = null!;
    public string Status { get; set; } = null!;
}
