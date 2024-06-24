using Legacy.Domain.Common;

namespace Legacy.Domain.Events;

public record OrderCreated(int OrderId) : DomainEvent;
