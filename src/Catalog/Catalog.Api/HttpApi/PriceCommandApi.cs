using Catalog.Prices;
using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;
using static Catalog.Api.Commands.PriceCommands;

namespace Catalog.Api.HttpApi;

[Route("/price")]
public class PriceCommandApi(ICommandService<Price> service) : CommandHttpApiBase<Price>(service)
{
    [HttpPost]
    [Route("initialize")]
    public Task<ActionResult<Result>> Draft([FromBody] Initialize cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("activate")]
    public Task<ActionResult<Result>> Activate([FromBody] Activate cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("deprecate")]
    public Task<ActionResult<Result>> Deprecate([FromBody] Deprecate cmd, CancellationToken ct)
        => Handle(cmd, ct);
}
