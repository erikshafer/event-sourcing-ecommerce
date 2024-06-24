using Eventuous;

namespace ShoppingCart.Products;

public record ProductId(string Value) : Id(Value);
