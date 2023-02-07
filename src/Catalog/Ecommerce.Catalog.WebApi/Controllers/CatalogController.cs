using Ecommerce.Catalog.Products;
using Marten;
using Marten.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Catalog.WebApi.Controllers;

public class CatalogController : ControllerBase
{
    [HttpGet("/products")]
    public Task<string> GetSku()
    {
        return Task.FromResult("Hello products!");
    }

    [HttpGet("/products/{productId}")]
    public Task GetSku(Guid productId, [FromServices] IQuerySession session)
    {
        return session.Json.WriteById<Product>(productId, HttpContext);
    }
}
