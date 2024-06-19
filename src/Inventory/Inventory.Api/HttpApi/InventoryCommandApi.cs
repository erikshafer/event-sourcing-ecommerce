using Eventuous;
using Eventuous.AspNetCore.Web;
using Inventory.Inventories;
using Microsoft.AspNetCore.Mvc;
using static Inventory.Inventories.InventoryCommands.V1;

namespace Inventory.Api.HttpApi;

[Route("/inventory")]
public class InventoryCommandApi : CommandHttpApiBaseFunc<InventoryState>
{
    private readonly IFuncCommandService<InventoryState> _service;

    public InventoryCommandApi(IFuncCommandService<InventoryState> service) : base(service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("initialize")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] InitializeInventory cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("stock-procurement")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] StockInventoryFromProcurementOrder cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("increment")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] IncrementInventory cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("decrement")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] DecrementInventory cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }
}
