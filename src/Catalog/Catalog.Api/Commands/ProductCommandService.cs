using Catalog.Products;
using Ecommerce.Core.Identities;
using Eventuous;
using static Catalog.Api.Commands.ProductCommands;

namespace Catalog.Api.Commands;

public class ProductCommandService : CommandService<Product, ProductState, ProductId>
{
    [Obsolete("Obsolete usage of OnNewAsync per Eventuous; use new API instead (TODO)")]
    public ProductCommandService(
        IAggregateStore store,
        Services.IsSkuAvailable isSkuAvailable,
        Services.IsUserAuthorized isUserAuthorized,
        ISnowflakeIdGenerator idGenerator)
        : base(store)
    {
        // On<InitializeProduct>(); // TODO use new API instead of obsolete versions

        OnNewAsync<DraftWithProvidedId>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd, _) => product.Draft(
                cmd.ProductId,
                cmd.Sku,
                cmd.Name,
                cmd.Description,
                cmd.Brand,
                DateTimeOffset.Now,
                cmd.CreatedBy,
                isSkuAvailable,
                isUserAuthorized)));

        var generatedId = idGenerator.New();
        OnNewAsync<Draft>(cmd => new ProductId(generatedId),
            ((product, cmd, _) => product.Draft(
                generatedId,
                cmd.Sku,
                cmd.Name,
                cmd.Description,
                cmd.Brand,
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
