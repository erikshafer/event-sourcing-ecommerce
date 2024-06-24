using Eventuous;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Carts;

namespace ShoppingCart.Api.HttpApi.Carts;

[Route("/carts")]
public class CartQueryApi : ControllerBase
{
    private readonly IAggregateStore _store;

    public CartQueryApi(IAggregateStore store) => _store = store;

    [HttpGet]
    [Route("{id}")]
    public async Task<CartState> Get(string id, CancellationToken ct)
    {
        // TODO: Is there a way to query the AggregateStory without a proper Aggregate, and just State?
        var product = await _store.Load<Cart>(StreamName.For<Cart>(id), ct);
        return product.State;
    }
}
