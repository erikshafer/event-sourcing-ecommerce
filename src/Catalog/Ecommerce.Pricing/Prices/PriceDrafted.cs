using Wolverine.Attributes;

namespace Ecommerce.Pricing.Prices;

public record PriceDrafted(Guid PriceId, Guid ProductId);

public record DraftPrice(Guid PriceId, Guid ProductId);

[WolverineHandler]
public class DraftPriceHandler
{
    // TODO
}
