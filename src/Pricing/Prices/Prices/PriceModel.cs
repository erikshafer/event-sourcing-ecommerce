using MicroPlumberd;

namespace Prices.Prices;

/// <summary>
/// Work in progress
/// ref:
/// </summary>
/// <see cref="https://modelingevolution.github.io/micro-plumberd/index.html#write-a-read-modelprocessor">docs</see>
[EventHandler]
public partial class PriceModel
{
    private Task Given(Metadata m, FooCreated ev)
    {
        // your code
        return Task.CompletedTask;
    }
    private Task Given(Metadata m, FooRefined ev)
    {
        // your code
        return Task.CompletedTask;
    }
}
