using Eventuous;

namespace ShoppingCart.Prices;

public record PriceId(string Value) : Id(Value);
