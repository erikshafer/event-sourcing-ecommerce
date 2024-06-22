using Catalog.Prices;
using Eventuous;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.HttpApi.Prices;

[Route("/prices")]
public class PriceQueryApi : ControllerBase
{
    private readonly IAggregateStore _store;

    public PriceQueryApi(IAggregateStore store) => _store = store;

    [HttpGet]
    [Route("{id}")]
    public async Task<PriceState> GetPrice(string id, CancellationToken ct)
    {
        var product = await _store.Load<Price>(StreamName.For<Price>(id), ct);
        return product.State;
    }
}
