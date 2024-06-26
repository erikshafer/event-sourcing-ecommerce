using Catalog.Products;
using Ecommerce.Core.Identities;
using Eventuous;

namespace Catalog.Api.Commands.Products;

public class ProductCommandService : CommandService<Product, ProductState, ProductId>
{
    [Obsolete("Obsolete usage of OnNewAsync per Eventuous; use new API instead (TODO)")]
    public ProductCommandService(
        IAggregateStore store,
        Services.IsSkuAvailable isSkuAvailable,
        Services.IsUserAuthorized isUserAuthorized,
        ICombIdGenerator idGenerator)
        : base(store)
    {
        // On<InitializeProduct>(); // TODO use new API instead of obsolete versions

        OnNewAsync<ProductCommands.DraftWithProvidedId>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd, _) => product.Draft(
                cmd.ProductId,
                cmd.Sku,
                cmd.Name,
                cmd.Description,
                cmd.Brand,
                cmd.Measurements,
                DateTimeOffset.Now,
                cmd.CreatedBy,
                isSkuAvailable,
                isUserAuthorized)));

        var generatedId = idGenerator.New();
        OnNewAsync<ProductCommands.Draft>(cmd => new ProductId(generatedId),
            ((product, cmd, _) => product.Draft(
                generatedId,
                cmd.Sku,
                cmd.Name,
                cmd.Description,
                cmd.Brand,
                cmd.Measurements,
                DateTimeOffset.Now,
                cmd.CreatedBy,
                isSkuAvailable,
                isUserAuthorized)));

        OnExisting<ProductCommands.Activate>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.Activate(
                DateTimeOffset.Now,
                cmd.ActivatedBy)));

        OnExisting<ProductCommands.Archive>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.Archive(
                DateTimeOffset.Now,
                cmd.ArchivedBy,
                cmd.Reason)));

        OnExisting<ProductCommands.Cancel>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.CancelDraft(
                DateTimeOffset.Now,
                cmd.CancelledBy,
                cmd.Reason)));

        OnExisting<ProductCommands.AdjustDescription>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.AdjustDescription(
                cmd.Description,
                DateTimeOffset.Now,
                cmd.AdjustedBy)));

        OnExisting<ProductCommands.AdjustName>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.AdjustName(
                cmd.Name,
                DateTimeOffset.Now,
                cmd.AdjustedBy)));

        OnExisting<ProductCommands.AdjustBrand>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.AdjustBrand(
                cmd.Brand,
                DateTimeOffset.Now,
                cmd.AdjustedBy)));

        OnExisting<ProductCommands.TakeMeasurement>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.TakeMeasurement(
                Measurement.GetName(cmd.Type), // TODO evaluate if this is a good path to take
                cmd.Unit,
                cmd.Value)));

        OnExisting<ProductCommands.RemoveMeasurement>(cmd => new ProductId(cmd.ProductId),
            ((product, cmd) => product.RemoveMeasurement(
                Measurement.GetName(cmd.Type)))); // TODO evaluate if this is a good path to take;
    }
}
