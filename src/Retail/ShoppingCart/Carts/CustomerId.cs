using Eventuous;

namespace ShoppingCart.Carts;

public record CustomerId(string Value) : Id(Value);
