using Eventuous;

namespace Catalog;

public record Reason
{
    public string Value { get; internal init; } = string.Empty;

    internal Reason() { }

    public Reason(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Reason value cannot be empty");

        Value = value;
    }

    public bool HasSameValue(Reason another) => Value == another.Value;

    public static implicit operator string(Reason reason) => reason.Value;
}
