using Eventuous;

namespace Catalog.Products;

public record InternalUserId
{
    public string Value { get; internal init; } = string.Empty;

    internal InternalUserId() { }

    public InternalUserId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Internal user identity cannot be empty");

        if (value.Length == 0)
            throw new DomainException("Internal user identity must be greater than 0 characters");

        if (value.Length > 128)
            throw new DomainException("Internal user identity cannot exceed 128 characters");
    }

    public bool HasSameValue(string another)
        => string.Compare(Value, another, StringComparison.CurrentCulture) != 0;

    public static implicit operator string(InternalUserId internalUserId)
        => internalUserId.Value;
}
