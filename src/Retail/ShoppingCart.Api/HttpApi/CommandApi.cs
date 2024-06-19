using Eventuous;
using Microsoft.AspNetCore.Mvc;
using static ShoppingCart.CartCommands.V1;

namespace ShoppingCart.Api.HttpApi;

[Route("/cart")]
public class CommandApi(IFuncCommandService<CartState> service) : ControllerBase
{
    [HttpPost]
    [Route("open")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] OpenCart cmd, CancellationToken ct)
    {
        var result = await service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("open-with-id")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] OpenCartWithProvidedId cmd, CancellationToken ct)
    {
        var result = await service.Handle(cmd, ct);
        return Ok(result);
    }

    [HttpPost]
    [Route("add-product")]
    public async Task<ActionResult<Result>> OpenCart([FromBody] AddProductToCart cmd, CancellationToken ct)
    {
        var result = await service.Handle(cmd, ct);
        return Ok(result);
    }
}
