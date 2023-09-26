using Eventuous;

namespace Catalog.Products;

public record ProductState : State<ProductState>
{
    public Sku Sku { get; init; } = default!;
    public string Code { get; init; } = default!;
}
