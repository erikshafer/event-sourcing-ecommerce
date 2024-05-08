using Catalog.Products;
using Eventuous;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.HttpApi;

[Route("/products")]
public class QueryApi : ControllerBase
{
    private readonly IAggregateStore _store;

    public QueryApi(IAggregateStore store) => _store = store;

    [HttpGet]
    [Route("{id}")]
    public async Task<ProductState> GetProduct(string id, CancellationToken ct)
    {
        var product = await _store.Load<Product>(StreamName.For<Product>(id), ct);
        return product.State;
    }
}
