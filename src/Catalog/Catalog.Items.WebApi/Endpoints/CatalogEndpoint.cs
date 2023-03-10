using Catalog.Items.Products;
using Catalog.Items.WebApi.Endpoints.Requests;
using Ecommerce.Core.Exceptions;
using Ecommerce.Core.Ids;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace Catalog.Items.WebApi.Endpoints;

public class CatalogEndpoint
{
    [WolverineGet("/products")]
    public static Task<IReadOnlyList<Product>> GetAll([FromServices] IQuerySession session, CancellationToken token) =>
        session.Query<Product>().ToListAsync(token);

    [WolverineGet("/products/{id}")]
    public static Task<Product?> GetProduct([FromRoute] Guid id, [FromServices] IQuerySession session, CancellationToken token) =>
        session.Events.AggregateStreamAsync<Product>(id, token: token);

    [WolverinePost("/products/draft")]
    public static async Task<IResult> DraftProduct(
        [FromBody] DraftProductRequest request,
        [FromServices] IIdGenerator idGenerator,
        [FromServices] IDocumentSession session,
        [FromServices] IMessageBus bus)
    {
        // In the future it would be nice to demonstrate being
        // dependent on legacy components -- or just completely remove.
        // Could also show the business process of validating a SKU as only
        // one should be active.
        var (sku, brandId, categoryId) = request;
        sku = string.IsNullOrEmpty(sku) ? "36606" : sku;
        var productId = idGenerator.New();
        var command = new DraftProduct(productId, sku, brandId, categoryId);

        await bus.InvokeAsync(command);

        // This should be its own encapsulated query.
        // This getting used to Wolverine idioms and deciding
        // how this application should act as well.
        var product = await session.Events.AggregateStreamAsync<Product>(productId, token: CancellationToken.None)
                      ?? throw AggregateNotFoundException.For<Product>(productId);

        return Results.Created($"/products/{command.ProductId}", product);
    }

    [WolverinePost("/products/confirm")]
    public static async Task<IResult> ConfirmProduct([FromBody] ConfirmProduct? command, [FromServices] IMessageBus bus)
    {
        await bus.InvokeAsync(command!);

        return Results.Ok();
    }

    [WolverinePost("/products/cancel")]
    public static Task CancelProduct([FromBody] CancelProduct? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);
}
