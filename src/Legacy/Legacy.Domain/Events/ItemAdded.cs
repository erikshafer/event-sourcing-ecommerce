using Legacy.Domain.Common;

namespace Legacy.Domain.Events;

public record ItemAdded(int ItemId) : DomainEvent;
