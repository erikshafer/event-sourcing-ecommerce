using Legacy.Domain.Common;

namespace Legacy.Domain.Events;

public record ItemChanged(int ItemId) : DomainEvent;
