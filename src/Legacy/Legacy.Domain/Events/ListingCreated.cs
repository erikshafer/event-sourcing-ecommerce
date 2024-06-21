using Legacy.Domain.Common;

namespace Legacy.Domain.Events;

public record ListingCreated(int ItemId, int ListingId) : DomainEvent;
