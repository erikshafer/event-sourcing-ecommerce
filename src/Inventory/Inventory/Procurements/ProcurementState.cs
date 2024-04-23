using Eventuous;

namespace Inventory.Procurements;

public record ProcurementState : State<ProcurementState>
{
    public ProcurementId Id { get; init; } = null!;

    public ProcurementStatus Status { get; init; } = ProcurementStatus.Unset;

    public ProcurementState()
    {
        On<ProcurementEvents.V1.ProcurementOrderPlaced>(Handle);
        On<ProcurementEvents.V1.ProcurementOrderReceived>(Handle);
        On<ProcurementEvents.V1.ProcurementOrderVerified>(Handle);
    }

    private static ProcurementState Handle(
        ProcurementState state,
        ProcurementEvents.V1.ProcurementOrderPlaced @event) =>
        state with {
            Id = new ProcurementId(@event.ProcurementId),
            Status = ProcurementStatus.Initialized
        };

    private static ProcurementState Handle(
        ProcurementState state,
        ProcurementEvents.V1.ProcurementOrderReceived @event) =>
        state with {
            Id = new ProcurementId(@event.ProcurementId),
            Status = ProcurementStatus.OrderReceived
        };

    private static ProcurementState Handle(
        ProcurementState state,
        ProcurementEvents.V1.ProcurementOrderVerified @event) =>
        state with {
            Id = new ProcurementId(@event.ProcurementId),
            Status = ProcurementStatus.Initialized
        };
}
