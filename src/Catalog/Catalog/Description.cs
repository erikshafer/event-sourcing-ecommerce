using Eventuous;

namespace Catalog;

public record Description
{
    public string Value { get; internal init; } = string.Empty;

    internal Description() { }

    public Description(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("A description must be provided");

        if (value.Length > 500)
            throw new DomainException("A product's internal description value cannot exceed 500 characters");

        Value = value;
    }
}
