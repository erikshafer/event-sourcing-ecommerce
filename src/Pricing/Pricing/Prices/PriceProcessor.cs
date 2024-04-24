using MicroPlumberd;

namespace Pricing.Prices;

[EventHandler]
public partial class PriceProcessor(IPlumber plumber)
{
    private async Task Given(Metadata m, FooRefined ev)
    {
        var id = Guid.NewGuid();
        var aggregate = new PriceAggregate(id);
        aggregate.Open(ev.Name + " new");
        await plumber.SaveNew(aggregate);
    }
}
