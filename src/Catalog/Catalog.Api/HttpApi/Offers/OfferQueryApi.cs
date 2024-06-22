using Catalog.Offers;
using Eventuous;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.HttpApi.Offers;

[Route("/offers")]
public class OfferQueryApi : ControllerBase
{
    private readonly IAggregateStore _store;

    public OfferQueryApi(IAggregateStore store) => _store = store;

    [HttpGet]
    [Route("{id}")]
    public async Task<OfferState> GetProduct(string id, CancellationToken ct)
    {
        var product = await _store.Load<Offer>(StreamName.For<Offer>(id), ct);
        return product.State;
    }
}
