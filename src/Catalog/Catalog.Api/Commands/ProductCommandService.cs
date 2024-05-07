using Catalog.Products;
using Eventuous;
using static Catalog.Api.Commands.ProductCommands;

namespace Catalog.Api.Commands;

public class ProductCommandService : CommandService<Product, ProductState, ProductId>
{
    [Obsolete("Obsolete usage of OnNewAsync, per Eventuous")]
    public ProductCommandService(
        IAggregateStore store,
        Services.IsProductSkuAvailable isProductSkuAvailable)
        : base(store)
    {
        // On<InitializeProduct>(); // TODO use new API instead of obsolete versions

        OnNewAsync<InitializeProduct>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd, _) => product.InitializeProduct(
                cmd.ProductId,
                cmd.Sku,
                cmd.Name,
                cmd.ShortDescription,
                cmd.LongDescription,
                isProductSkuAvailable)));

        OnExisting<ConfirmProduct>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.ConfirmProduct(
                cmd.ConfirmedBy,
                DateTimeOffset.Now)));
    }
}
