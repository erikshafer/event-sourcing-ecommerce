using Ecommerce.Catalog.Products;
using Ecommerce.Catalog.WebApi.Requests;
using Marten;
using Marten.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace Ecommerce.Catalog.WebApi.Controllers;

public class CatalogController : ControllerBase
{
    [HttpGet("/products")]
    public Task GetAll([FromServices] IQuerySession session)
    {
        return session.Query<Product>().ToListAsync();
    }

    [HttpGet("/products/{productId:guid}")]
    public Task GetSku(Guid productId, [FromServices] IQuerySession session)
    {
        return session.Json.WriteById<Product>(productId, HttpContext);
    }

    [HttpPost("/products/draft")]
    public Task DraftProduct([FromBody] DraftProduct? command, [FromServices] IMessageBus bus)
    {
        return bus.InvokeAsync(command!);
    }

    [HttpPost("/products/establish-brand")]
    public Task EstablishBrand([FromBody] EstablishBrand? command, [FromServices] IMessageBus bus)
    {
        return bus.InvokeAsync(command!);
    }

    [HttpPost("/products/list-tags")]
    public Task ListTags([FromBody] ListTags? command, [FromServices] IMessageBus bus)
    {
        return bus.InvokeAsync(command!);
    }

    [HttpPost("/products/confirm")]
    public Task ConfirmProduct([FromBody] ConfirmProduct? command, [FromServices] IMessageBus bus)
    {
        return bus.InvokeAsync(command!);
    }

    [HttpPost("/products/cancel")]
    public Task CancelProduct([FromBody] CancelProduct? command, [FromServices] IMessageBus bus)
    {
        return bus.InvokeAsync(command!);
    }
}
