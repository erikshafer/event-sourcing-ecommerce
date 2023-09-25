namespace Catalog.Prices;

public record PriceInitialized(Guid PriceId, Guid ProductId);

public record InitializePrice(Guid PriceId, Guid ProductId);

public class InitializePriceHandler
{
    // TODO
}
