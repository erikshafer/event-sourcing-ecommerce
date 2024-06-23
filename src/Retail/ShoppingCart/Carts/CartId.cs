using Eventuous;

namespace ShoppingCart.Carts;

public record CartId(string Value) : Id(Value);
