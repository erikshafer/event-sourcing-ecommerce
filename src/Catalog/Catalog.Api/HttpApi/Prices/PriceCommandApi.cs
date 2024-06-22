using Catalog.Api.Commands.Prices;
using Catalog.Prices;
using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.HttpApi.Prices;

[Route("/price")]
public class PriceCommandApi(ICommandService<Price> service) : CommandHttpApiBase<Price>(service)
{
    [HttpPost]
    [Route("initialize")]
    public Task<ActionResult<Result>> Draft([FromBody] PriceCommands.Initialize cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("activate")]
    public Task<ActionResult<Result>> Activate([FromBody] PriceCommands.Activate cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("deprecate")]
    public Task<ActionResult<Result>> Deprecate([FromBody] PriceCommands.Deprecate cmd, CancellationToken ct)
        => Handle(cmd, ct);
}
