namespace Catalog.Api.Commands;

public static class ProductCommands
{
    public record Initialize(
        string ProductId,
        string Sku,
        string Name
    );

    public record DraftDescription(
        string ProductId,
        string Description,
        string WrittenBy
    );

    public record Confirm(
        string ProductId,
        string ConfirmedBy);
}
