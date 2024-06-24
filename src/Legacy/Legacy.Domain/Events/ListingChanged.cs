using Legacy.Domain.Common;

namespace Legacy.Domain.Events;

public record ListingChanged(int ItemId, int ListingId) : DomainEvent;
