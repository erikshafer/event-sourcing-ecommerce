using Wolverine.Http;

namespace Ecommerce.Catalog.WebApi.Endpoints;

public class HelloEndpoint
{
    [WolverineGet("/")]
    public string Get() => "Hello.";
}
