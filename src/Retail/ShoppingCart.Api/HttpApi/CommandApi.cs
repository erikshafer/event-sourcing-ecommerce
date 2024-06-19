using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;
using static ShoppingCart.CartCommands.V1;

namespace ShoppingCart.Api.HttpApi;

[Route("/cart")]
public class CommandApi : CommandHttpApiBaseFunc<CartState>
{
    private readonly IFuncCommandService<CartState> _service;

    public CommandApi(IFuncCommandService<CartState> service) : base(service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("open")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] OpenCart cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("open-with-id")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] OpenCartWithProvidedId cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("add-product")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] AddProductToCart cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("remove-product")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] RemoveProductFromCart cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("confirm")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] ConfirmCart cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }
}
