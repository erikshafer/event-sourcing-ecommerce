using Catalog.Api.Commands.Offers;
using Catalog.Offers;
using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.HttpApi.Offers;

[Route("/offer")]
public class OfferCommandApi(ICommandService<Offer> service) : CommandHttpApiBase<Offer>(service)
{
    [HttpPost]
    [Route("draft")]
    public Task<ActionResult<Result>> Draft([FromBody] OfferCommands.Draft cmd, CancellationToken ct)
        => Handle(cmd, ct);

    [HttpPost]
    [Route("activate")]
    public Task<ActionResult<Result>> Activate([FromBody] OfferCommands.Activate cmd, CancellationToken ct)
        => Handle(cmd, ct);
}
