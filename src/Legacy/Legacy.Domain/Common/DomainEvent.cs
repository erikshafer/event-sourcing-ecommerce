namespace Legacy.Domain.Common;

public abstract record DomainEvent : IDomainEvent
{
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}
