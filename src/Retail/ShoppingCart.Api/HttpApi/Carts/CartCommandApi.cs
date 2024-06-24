using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Carts;

namespace ShoppingCart.Api.HttpApi.Carts;

[Route("/cart")]
public class CartCommandApi : CommandHttpApiBaseFunc<CartState>
{
    private readonly IFuncCommandService<CartState> _service;

    public CartCommandApi(IFuncCommandService<CartState> service) : base(service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("open")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] CartCommands.V1.OpenCart cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("add-product")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] CartCommands.V1.AddProductToCart cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("remove-product")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] CartCommands.V1.RemoveProductFromCart cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("confirm")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] CartCommands.V1.ConfirmCart cmd, CancellationToken ct)
    {
        var result = await _service.Handle(cmd, ct);
        return Ok(result);
    }
}
