using Eventuous;

namespace Catalog.Products;

public class Brand
{
    public string Value { get; internal init; } = string.Empty;

    internal Brand() { }

    public Brand(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Brand value cannot be empty");

        if (value.Length <= 2)
            throw new DomainException("A brand's name must exceed 2 characters");

        if (value.Length > 64)
            throw new DomainException("A brand's name cannot exceed 64 characters");

        Value = value;
    }

    public bool HasSameValue(string another)
        => string.Compare(Value, another, StringComparison.CurrentCulture) == 0;

    public static implicit operator string(Brand brand)
        => brand.Value;
}
