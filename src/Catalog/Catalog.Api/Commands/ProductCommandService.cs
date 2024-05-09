using Catalog.Products;
using Eventuous;
using static Catalog.Api.Commands.ProductCommands;

namespace Catalog.Api.Commands;

public class ProductCommandService : CommandService<Product, ProductState, ProductId>
{
    [Obsolete("Obsolete usage of OnNewAsync per Eventuous; use new API instead (TODO)")]
    public ProductCommandService(
        IAggregateStore store,
        Services.IsSkuAvailable isSkuAvailable,
        Services.IsUserAuthorized isUserAuthorized)
        : base(store)
    {
        // On<InitializeProduct>(); // TODO use new API instead of obsolete versions

        OnNewAsync<Draft>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd, _) => product.Draft(
                cmd.ProductId,
                cmd.Sku,
                cmd.Name,
                cmd.Description,
                DateTimeOffset.Now,
                cmd.CreatedBy,
                isSkuAvailable,
                isUserAuthorized)));

        OnExisting<Activate>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.Activate(
                DateTimeOffset.Now,
                cmd.ActivatedBy)));

        OnExisting<Archive>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.Archive(
                DateTimeOffset.Now,
                cmd.ArchivedBy,
                cmd.Reason)));

        OnExisting<Cancel>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.CancelDraft(
                DateTimeOffset.Now,
                cmd.CancelledBy,
                cmd.Reason)));

        OnExisting<AdjustName>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.AdjustName(
                cmd.Name,
                DateTimeOffset.Now,
                cmd.AdjustedBy)));

        OnExisting<AdjustDescription>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.AdjustDescription(
                cmd.Description,
                DateTimeOffset.Now,
                cmd.AdjustedBy)));
    }
}
