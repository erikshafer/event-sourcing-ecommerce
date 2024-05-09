using Eventuous;

namespace Catalog;

public record Name
{
    public string Value { get; internal init; } = string.Empty;

    internal Name() { }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Name value cannot be empty");

        if (value.Length <= 4)
            throw new DomainException("A product's name must exceed 4 characters");

        if (value.Length > 200)
            throw new DomainException("A product's name cannot exceed 200 characters");
    }

    public bool HasSameValue(string another)
        => string.Compare(Value, another, StringComparison.CurrentCulture) != 0;

    public static implicit operator string(Name name)
        => name.Value;
}
