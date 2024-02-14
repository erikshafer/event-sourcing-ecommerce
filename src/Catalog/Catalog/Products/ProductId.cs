using Eventuous;

namespace Catalog.Products;

public record ProductId(string Value) : Id(Value);
