using Wolverine.Http;

namespace Ecommerce.Catalog.WebApi.Controllers;

public class HelloEndpoint
{
    [WolverineGet("/")]
    public string Get() => "Hello.";
}
