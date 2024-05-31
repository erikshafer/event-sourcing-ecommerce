using Eventuous;

namespace Catalog;

public record Creation
{
    public DateTimeOffset CreatedAt { get; }
    public InternalUserId InternalUserId { get; } = null!;

    internal Creation() { }

    public Creation(DateTimeOffset createdAt, InternalUserId internalUserId)
    {
        if (createdAt == default)
            throw new DomainException("Creation date must be set");

        if (internalUserId == default)
            throw new DomainException("Internal user identity must be set");

        (CreatedAt, InternalUserId) = (createdAt, internalUserId);
    }

    public Creation(DateTimeOffset createdAt, string internalUserId)
    {
        (CreatedAt, InternalUserId) = (createdAt, new InternalUserId(internalUserId));
    }
}
