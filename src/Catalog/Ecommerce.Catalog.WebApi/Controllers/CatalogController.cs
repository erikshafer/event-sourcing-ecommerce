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
    public Task<IReadOnlyList<Product>> GetAll([FromServices] IQuerySession session, CancellationToken token) =>
        session.Query<Product>().ToListAsync(token);

    [WolverineGet("/products/{productId}")]
    public Task GetSku(Guid productId, [FromServices] IQuerySession session) =>
        session.Json.WriteById<Product>(productId, HttpContext);

    [WolverinePost("/products/draft")]
    public Task DraftProduct([FromBody] DraftProduct? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);

    [WolverinePost("/products/establish-brand")]
    public Task EstablishBrand([FromBody] EstablishBrand? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);

    [WolverinePost("/products/list-tags")]
    public Task ListTags([FromBody] ListTags? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);

    [WolverinePost("/products/confirm")]
    public Task ConfirmProduct([FromBody] ConfirmProduct? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);

    [WolverinePost("/products/cancel")]
    public Task CancelProduct([FromBody] CancelProduct? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);
}
