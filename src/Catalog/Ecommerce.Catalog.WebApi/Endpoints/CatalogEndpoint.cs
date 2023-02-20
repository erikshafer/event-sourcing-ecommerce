using Ecommerce.Catalog.Products;
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
    public static async Task<IResult> DraftProduct([FromBody] DraftProduct command, [FromServices] IDocumentSession session, [FromServices] IMessageBus bus)
    {
        await bus.InvokeAsync(command);

        var product = await session.LoadAsync<Product>(command.ProductId);

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
