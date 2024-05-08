using Catalog.Products;
using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;
using static Catalog.Api.Commands.ProductCommands;

namespace Catalog.Api.HttpApi;

[Route("/product")]
public class CommandApi(ICommandService<Product> service) : CommandHttpApiBase<Product>(service)
{
    [HttpPost]
    [Route("initialize")]
    public Task<ActionResult<Result>> InitializeProduct([FromBody] InitializeProduct cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("confirm")]
    public Task<ActionResult<Result>> ConfirmProduct([FromBody] ConfirmProduct cmd, CancellationToken ct)
        => Handle(cmd, ct);
}
