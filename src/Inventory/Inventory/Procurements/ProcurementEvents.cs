using Eventuous;

namespace Inventory.Procurements;

public static class ProcurementEvents
{
    public static class V1
    {
        [EventType("V1.ProcurementOrderPlaced")]
        public record ProcurementOrderPlaced(
            string ProcurementId,
            string BillOfLadingId
        );

        [EventType("V1.ProcurementOrderReceived")]
        public record ProcurementOrderReceived(
            string ProcurementId
        );

        [EventType("V1.ProcurementOrderVerified")]
        public record ProcurementOrderVerified(
            string ProcurementId
        );

        [EventType("V1.ProcurementInventoryStocked")]
        public record ProcurementInventoryStocked(
            string ProcurementId
        );
    }
}
