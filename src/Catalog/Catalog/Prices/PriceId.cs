using Eventuous;

namespace Catalog.Prices;

public record PriceId(string Value) : Id(Value);
