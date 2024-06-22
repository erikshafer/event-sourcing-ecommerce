using Catalog.Api.Commands;
using Catalog.Products;
using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.HttpApi.Products;

[Route("/product")]
public class ProductCommandApi(ICommandService<Product> service) : CommandHttpApiBase<Product>(service)
{
    [HttpPost]
    [Route("draft-with-id")]
    public Task<ActionResult<Result>> Draft([FromBody] ProductCommands.DraftWithProvidedId cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("draft")]
    public Task<ActionResult<Result>> Draft([FromBody] ProductCommands.Draft cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("activate")]
    public Task<ActionResult<Result>> Activate([FromBody] ProductCommands.Activate cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("archive")]
    public Task<ActionResult<Result>> Archive([FromBody] ProductCommands.Archive cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("cancel")]
    public Task<ActionResult<Result>> Cancel([FromBody] ProductCommands.Cancel cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("adjust-description")]
    public Task<ActionResult<Result>> AdjustDescription([FromBody] ProductCommands.AdjustDescription cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("adjust-name")]
    public Task<ActionResult<Result>> AdjustName([FromBody] ProductCommands.AdjustName cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("adjust-brand")]
    public Task<ActionResult<Result>> AdjustBrand([FromBody] ProductCommands.AdjustBrand cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("take-measurement")]
    public Task<ActionResult<Result>> TakeMeasurement([FromBody] ProductCommands.TakeMeasurement cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("remove-measurement")]
    public Task<ActionResult<Result>> RemoveMeasurement([FromBody] ProductCommands.RemoveMeasurement cmd, CancellationToken ct)
        => Handle(cmd, ct);
}
