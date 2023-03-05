using Ecommerce.Catalog.Products;
using Ecommerce.Catalog.WebApi.Endpoints.Requests;
using Ecommerce.Core.Exceptions;
using Ecommerce.Core.Ids;
using Marten;
using Marten.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace Ecommerce.Catalog.WebApi.Endpoints;

public class CatalogEndpoint
{
    [WolverineGet("/products")]
    public static Task<IReadOnlyList<Product>> GetAll([FromServices] IQuerySession session, CancellationToken token) =>
        session.Query<Product>().ToListAsync(token);

    [WolverineGet("/products/{id}")]
    public static Task<Product?> GetSku(Guid id, [FromServices] IQuerySession session, CancellationToken ct) =>
        session.LoadAsync<Product>(id, ct);

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
        var sku = string.IsNullOrEmpty(request.Sku) ? "36606" : request.Sku;
        var productId = idGenerator.New();
        var command = new DraftProduct(productId, sku);

        await bus.InvokeAsync(command);

        // This should be its own encapsulated query.
        // This getting used to Wolverine idioms and deciding
        // how this application should act as well.
        var product = await session.Events.AggregateStreamAsync<Product>(productId, token: CancellationToken.None)
                      ?? throw AggregateNotFoundException.For<Product>(productId);

        return Results.Created($"/products/{command.ProductId}", product);
    }

    [WolverinePost("/products/establish-brand")]
    public static Task EstablishBrand([FromBody] EstablishBrand? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);

    [WolverinePost("/products/list-tags")]
    public static Task ListTags([FromBody] ListTags? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);

    [WolverinePost("/products/confirm")]
    public static Task ConfirmProduct([FromBody] ConfirmProduct? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);

    [WolverinePost("/products/cancel")]
    public static Task CancelProduct([FromBody] CancelProduct? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);
}
