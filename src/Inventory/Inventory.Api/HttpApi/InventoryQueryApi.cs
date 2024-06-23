using Eventuous;
using Inventory.Inventories;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.HttpApi;

[Route("/inventories")]
public class InventoryQueryApi : ControllerBase
{
    private readonly IAggregateStore _store;

    public InventoryQueryApi(IAggregateStore store) => _store = store;

    [HttpGet]
    [Route("{id}")]
    public async Task<InventoryState> Get(string id, CancellationToken ct)
    {
        // TODO: Is there a way to query the AggregateStory without a proper Aggregate, and just State?
        var product = await _store.Load<Inventories.Inventory>(StreamName.For<Inventories.Inventory>(id), ct);
        return product.State;
    }
}
