namespace Catalog.Api.Commands;

public static class ProductCommands
{
    public record DraftWithProvidedId(
        string ProductId,
        string Sku,
        string Name,
        string Description,
        string Brand,
        string CreatedBy
    );

    public record Draft(
        string Sku,
        string Name,
        string Description,
        string Brand,
        string CreatedBy
    );

    public record Activate(
        string ProductId,
        string ActivatedBy);

    public record Archive(
        string ProductId,
        string ArchivedBy,
        string Reason
    );

    public record Cancel(
        string ProductId,
        string CancelledBy,
        string Reason
    );

    public record AdjustDescription(
        string ProductId,
        string Description,
        string AdjustedBy
    );

    public record AdjustName(
        string ProductId,
        string Name,
        string AdjustedBy
    );

    public record AdjustBrand(
        string ProductId,
        string Brand,
        string AdjustedBy
    );

    public record TakeMeasurement(
        string ProductId,
        string Type,
        string Unit,
        string Value
    );

    public record RemoveMeasurement(
        string ProductId,
        string Type
    );
}
