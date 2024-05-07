using Eventuous;
using static Catalog.Products.ProductEvents;
using static Catalog.Products.Services;

namespace Catalog.Products;

public class Product : Aggregate<ProductState>
{
    public async Task InitializeProduct(
        string productId,
        string sku,
        string name,
        string shortDescription,
        string longDescription,
        IsProductSkuAvailable isProductSkuAvailable)
    {
        EnsureDoesntExist();
        await EnsureSkuAvailable(new Sku(sku), isProductSkuAvailable);

        // other domain logic, if applicable

        Apply(
            new V1.ProductInitialized(
                productId,
                sku,
                name,
                shortDescription,
                longDescription
                )
        );
    }

    public void ConfirmProduct(string confirmedBy, DateTimeOffset confirmedAt)
    {
        EnsureExists();

        Apply(
            new V1.ProductConfirmed(
                State.Id.Value,
                confirmedBy,
                confirmedAt
            )
        );
    }

    public void DeprecateProduct(string deprecatedBy, string reason)
    {
        EnsureExists();

        Apply(
            new V1.ProductDeprecated(
                State.Id.Value,
                deprecatedBy,
                reason
            )
        );
    }

    public void CancelProduct(string cancelledBy, string reason)
    {
        EnsureExists();

        Apply(
            new V1.ProductCancelled(
                State.Id.Value,
                cancelledBy,
                reason
            )
        );
    }

    private static async Task EnsureSkuAvailable(Sku sku, IsProductSkuAvailable isProductSkuAvailable)
    {
        var skuAvailable = await isProductSkuAvailable(sku);
        if (!skuAvailable)
            throw new DomainException("SKU not available");
    }
}
