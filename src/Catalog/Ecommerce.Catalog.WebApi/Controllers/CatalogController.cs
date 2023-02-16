using Ecommerce.Catalog.Products;
using Ecommerce.Catalog.WebApi.Requests;
using Marten;
using Marten.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace Ecommerce.Catalog.WebApi.Controllers;

public class CatalogController : ControllerBase
{
    [WolverineGet("/products")]
    public static Task<IReadOnlyList<Product>> GetAll([FromServices] IQuerySession session, CancellationToken token) =>
        session.Query<Product>().ToListAsync(token);

    // TODO
    [WolverineGet("/products/{productId}")]
    public Task GetSku(Guid productId, [FromServices] IQuerySession session) =>
        session.Json.WriteById<Product>(productId, HttpContext);

    [WolverinePost("/products/draft")]
    public static Task DraftProduct([FromBody] DraftProduct? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);

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
