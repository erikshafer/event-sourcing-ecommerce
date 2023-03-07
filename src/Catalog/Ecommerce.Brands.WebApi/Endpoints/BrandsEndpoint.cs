using Ecommerce.Brands.Brands;
using Ecommerce.Brands.WebApi.Endpoints.Requests;
using Ecommerce.Core.Exceptions;
using Ecommerce.Core.Ids;
using Marten;
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
        session.LoadAsync<Brand>(id, token);

    [WolverinePost("/brands/initialize")]
    public static async Task<IResult> DraftProduct(
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
}
