using Catalog.Products;
using Eventuous;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.HttpApi.Products;

[Route("/products")]
public class ProductQueryApi : ControllerBase
{
    private readonly IAggregateStore _store;

    public ProductQueryApi(IAggregateStore store) => _store = store;

    [HttpGet]
    [Route("{id}")]
    public async Task<ProductState> Get(string id, CancellationToken ct)
    {
        var product = await _store.Load<Product>(StreamName.For<Product>(id), ct);
        return product.State;
    }
}
