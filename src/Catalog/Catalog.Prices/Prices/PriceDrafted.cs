using Wolverine.Attributes;

namespace Catalog.Prices.Prices;

public record PriceDrafted(Guid PriceId, Guid ProductId);

public record DraftPrice(Guid PriceId, Guid ProductId);

[WolverineHandler]
public class DraftPriceHandler
{
    // TODO
}
