using Ecommerce.Brands.Brands;
using Ecommerce.Brands.WebApi.Endpoints.Requests;
using Ecommerce.Core.Exceptions;
using Ecommerce.Core.Ids;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using Wolverine.Http;

namespace Ecommerce.Brands.WebApi.Endpoints;

public class BrandsEndpoint
{
    [WolverineGet("/brands")]
    public static Task<IReadOnlyList<Brand>> GetAll([FromServices] IQuerySession session, CancellationToken token) =>
        session.Query<Brand>().ToListAsync(token);

    [WolverineGet("/brands/{id}")]
    public static Task<Brand?> GetBrand([FromRoute] Guid id, [FromServices] IQuerySession session, CancellationToken token) =>
        session.Events.AggregateStreamAsync<Brand>(id, token: token);

    [WolverinePost("/brands/initialize")]
    public static async Task<IResult> InitializeBrand(
        [FromBody] InitializeBrandRequest request,
        [FromServices] IIdGenerator idGenerator,
        [FromServices] IDocumentSession session,
        [FromServices] IMessageBus bus)
    {
        var name = request.Name;
        var brandId = idGenerator.New();
        var command = new InitializeBrand(brandId, name);

        await bus.InvokeAsync(command);

        var brand = await session.Events.AggregateStreamAsync<Brand>(brandId, token: CancellationToken.None)
                    ?? throw AggregateNotFoundException.For<Brand>(brandId);

        return Results.Created($"/brands/{command.BrandId}", brand);
    }

    [WolverinePost("/brands/activate")]
    public static async Task<IResult> ActivateBrand([FromBody] ActivateBrand command, [FromServices] IMessageBus bus)
    {
        await bus.InvokeAsync(command);

        return Results.Ok();
    }

    [WolverinePost("/brands/deactivate")]
    public static Task DeactivateBrand([FromBody] DeactivateBrand? command, [FromServices] IMessageBus bus) =>
        bus.InvokeAsync(command!);
}
