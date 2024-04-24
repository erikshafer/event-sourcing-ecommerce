using MicroPlumberd;

namespace Pricing.Prices;

/// <summary>
/// Work in progress
/// ref:
/// </summary>
/// <see cref="https://modelingevolution.github.io/micro-plumberd/index.html#aggregates">docs</see>
[Aggregate(SnapshotEvery = 50)]
public partial class PriceAggregate(Guid id) : AggregateBase<Guid,PriceAggregate.FooState>(id)
{
    public record FooState { public string Name { get; set; } = null!; };
    private static FooState Given(FooState state, FooCreated ev) => state with { Name = ev.Name! };
    private static FooState Given(FooState state, FooRefined ev) => state with { Name =ev.Name! };
    public void Open(string msg) => AppendPendingChange(new FooCreated() { Name = msg });
    public void Change(string msg) => AppendPendingChange(new FooRefined() { Name = msg });
}
// And events:
public record FooCreated { public string? Name { get; set; } }
public record FooRefined { public string? Name { get; set; } }
