namespace Catalog.Api.Commands.Offers;

public class OfferCommands
{
    public record Draft(
        string Sku,
        string CreatedBy
    );

    public record Activate(
        string OfferId,
        string ActivatedBy
    );
}
