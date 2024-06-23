using Eventuous;

namespace ShoppingCart.Carts;

public record ProductId(string Value) : Id(Value);
