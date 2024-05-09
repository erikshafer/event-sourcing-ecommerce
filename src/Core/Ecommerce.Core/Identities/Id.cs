namespace Ecommerce.Core.Identities;

[Obsolete("Use Eventuous.Id instead, this internal Id will be repurposed or deleted")]
public abstract record Id
{
    public string Value { get; }
    public sealed override string ToString() => Value;
    public void Deconstruct(out string value) => value = Value;

    public static implicit operator string(Id? id) =>
        id?.ToString() ?? throw new InvalidOperationException();

    public static implicit operator long(Id? id)
    {
        var stringId = id?.ToString();
        var wasParsed = long.TryParse(stringId, out var longId);
        return wasParsed ? longId : throw new InvalidOperationException();
    }

    public static implicit operator Guid(Id? id)
    {
        var stringId = id?.ToString();
        var wasParsed = Guid.TryParse(stringId, out var guid);
        return wasParsed ? guid : throw new InvalidOperationException();
    }

    protected Id(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException();

        Value = value;
    }
}
