namespace Catalog.Api.Commands;

public static class PriceCommands
{
    public record Initialize(
        string Sku,
        decimal MinimumAdvertisedPrice,
        decimal ManufacturerSuggestedRetailPrice,
        int BundledQuantity,
        decimal BundledPrice,
        string Currency,
        DateTimeOffset CreatedAt,
        string CreatedBy
    );

    public record Activate(
        string PriceId,
        string ActivatedBy
    );

    public record Deprecate(
        string PriceId,
        string DeprecatedBy,
        string Reason
    );
}
