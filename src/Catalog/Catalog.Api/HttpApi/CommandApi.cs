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
    [Route("draft-with-id")]
    public Task<ActionResult<Result>> Draft([FromBody] DraftWithProvidedId cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("draft")]
    public Task<ActionResult<Result>> Draft([FromBody] Draft cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("activate")]
    public Task<ActionResult<Result>> Activate([FromBody] Activate cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("archive")]
    public Task<ActionResult<Result>> Archive([FromBody] Archive cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("cancel")]
    public Task<ActionResult<Result>> Cancel([FromBody] Cancel cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("adjust-description")]
    public Task<ActionResult<Result>> AdjustDescription([FromBody] AdjustDescription cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("adjust-name")]
    public Task<ActionResult<Result>> AdjustName([FromBody] AdjustName cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("adjust-brand")]
    public Task<ActionResult<Result>> AdjustBrand([FromBody] AdjustBrand cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("take-measurement")]
    public Task<ActionResult<Result>> TakeMeasurement([FromBody] TakeMeasurement cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("remove-measurement")]
    public Task<ActionResult<Result>> RemoveMeasurement([FromBody] RemoveMeasurement cmd, CancellationToken ct)
        => Handle(cmd, ct);
}
