using Catalog.Products;
using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;
using static Catalog.Api.Commands.ProductCommands;

namespace Catalog.Api.Endpoints;

[Route("/product")]
public class ProductApi : CommandHttpApiBase<Product>
{
    public ProductApi(ICommandService<Product> service) : base(service)
    {
    }

    [HttpPost]
    [Route("initialize")]
    public Task<ActionResult<Result>> InitializeProduct([FromBody] InitializeProduct cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("confirm")]
    public Task<ActionResult<Result>> ConfirmProduct([FromBody] ConfirmProduct cmd, CancellationToken ct)
        => Handle(cmd, ct);
}
