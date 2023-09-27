namespace Catalog.Products;

public sealed record ProductId : Id
{
    private ProductId(string value)
        : base(value) { }

    public static ProductId Empty() => new(string.Empty);
}

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
