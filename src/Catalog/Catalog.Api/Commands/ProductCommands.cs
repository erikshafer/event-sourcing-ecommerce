namespace Catalog.Api.Commands;

public static class ProductCommands
{
    public record InitializeProduct(
        string ProductId,
        string Sku,
        string Name
    );

    public record ConfirmProduct(
        string ProductId,
        string ConfirmedBy);
}
