namespace Ecommerce.Core.Identities;

public abstract record Id
{
    public string Value { get; }
    public sealed override string ToString() => Value;
    public void Deconstruct(out string value) => value = Value;

    public static implicit operator string(Id? id) =>
        id?.ToString() ?? throw new InvalidOperationException();

    protected Id(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException();

        Value = value;
    }
}
