using Legacy.Domain.Common;

namespace Legacy.Domain.Events;

public record OrderCompleted(int OrderId) : DomainEvent;
