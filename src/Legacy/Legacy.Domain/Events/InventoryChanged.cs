using Legacy.Domain.Common;

namespace Legacy.Domain.Events;

public record InventoryChanged(int InventoryId, int ItemId) : DomainEvent;
