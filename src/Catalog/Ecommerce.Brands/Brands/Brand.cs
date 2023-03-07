using Ecommerce.Core.Aggregates;
using Ecommerce.Core.Exceptions;

namespace Ecommerce.Brands.Brands;

public sealed class Brand : Aggregate
{
    public string Name { get; private set; } = default!;

    public BrandStatus Status { get; private set; }

    public Brand()
    {
    }

    // public Brand(BrandInitialized @event)
    // {
    //     Id = @event.BrandId;
    //     Name = @event.Name;
    //
    //     Status = BrandStatus.UnderNegotiation;
    // }

    private Brand(Guid brandId, string name)
    {
        var @event = new BrandInitialized(brandId, name);

        Enqueue(@event);
        Apply(@event);
    }

    public void Apply(BrandInitialized @event)
    {
        Id = @event.BrandId;
        Name = @event.Name;

        Status = BrandStatus.UnderNegotiation;
    }

    public void Activate()
    {
        // What sort of check would be appropriate?
        // Can brands go from Inactive to Active, or is a
        // new Brand required? How does the business differentiate?
        var @event = new BrandActivated(Id);

        Enqueue(@event);
        Apply(@event);
    }

    public void Apply(BrandActivated @event)
    {
        Status = BrandStatus.Active;
    }

    public void Deactivate()
    {
        var @event = new BrandDeactivated(Id);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(BrandDeactivated @event)
    {
        Status = BrandStatus.Inactive;
    }

    public void NameChange(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw InvalidAggregateOperationException.For<Brand>(Id, nameof(NameChange));

        var @event = new NameChanged(Id, name);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(NameChanged @event)
    {
        Name = @event.Name;
    }
}
