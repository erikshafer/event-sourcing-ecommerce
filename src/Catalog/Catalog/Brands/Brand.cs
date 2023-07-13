using Ecommerce.Core.Aggregates;

namespace Catalog.Brands;

public sealed class Brand : Aggregate
{
    public BrandName Name { get; private set; } = default!;

    public BrandStatus Status { get; private set; } = BrandStatus.Unset;

    public static Brand Prospect(Guid brandId, BrandName name)
    {
        return new Brand(brandId, name);
    }

    public Brand()
    {
    }

    private Brand(Guid brandId, BrandName name)
    {
        var @event = new BrandProspected(brandId, name.Value);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(BrandProspected @event)
    {
        Id = @event.BrandId;
        Name = new BrandName(@event.Name);
        Status = BrandStatus.Prospect;
    }

    public void ChangeName(BrandName name)
    {
        var isSameName = Name.Matches(name);
        if (isSameName)
            throw new ArgumentException($"Name is already current value: '{Name.Value}'");

        var @event = new BrandNameChanged(Id, name.Value);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(BrandNameChanged @event)
    {
        Name = new BrandName(@event.Name);
    }

    public void ContractInNegotiations()
    {
        var @event = new BrandContractInNegotiations(Id);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(BrandContractInNegotiations @event)
    {
        Status = BrandStatus.ContractInNegotiations;
    }

    public void ContractSigned()
    {
        var @event = new BrandContractInNegotiations(Id);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(BrandContractSigned @event)
    {
        Status = BrandStatus.ContractSigned;
    }

    public void AssignToOnboarding()
    {
        var @event = new BrandAssignedToOnboarding(Id);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(BrandAssignedToOnboarding @event)
    {
        Status = BrandStatus.InOnboarding;
    }

    public void Activate()
    {
        var @event = new BrandActivated(Id);

        Enqueue(@event);
        Apply(@event);
    }

    private void Apply(BrandActivated @event)
    {
        Status = BrandStatus.Active;
    }
}
