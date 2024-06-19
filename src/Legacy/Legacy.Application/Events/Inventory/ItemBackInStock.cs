using MediatR;

namespace Legacy.Application.Events.Inventory;

public record ItemBackInStock(int ItemId) : INotification;
