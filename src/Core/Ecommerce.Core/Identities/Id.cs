namespace Ecommerce.Core.Identities;

[Obsolete("Use Eventuous.Id instead, this internal Id will be deleted in the future.")]
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
