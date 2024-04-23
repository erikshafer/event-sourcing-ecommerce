using Eventuous;

using static Inventory.Procurements.ProcurementEvents;

namespace Inventory.Procurements;

public class Procurement : Aggregate<ProcurementState>
{
    public async Task OrderPlaced(
        string procurementId,
        string billOfLadingId,
        Services.IsBillOfLadingValid isBillOfLadingValid)
    {
        EnsureDoesntExist();
        await EnsureBillOfLadingValid(new BillOfLadingId(billOfLadingId), isBillOfLadingValid);

        Apply(new V1.ProcurementOrderPlaced(
            procurementId,
            billOfLadingId));
    }

    private static async Task EnsureBillOfLadingValid(
        BillOfLadingId billOfLadingId,
        Services.IsBillOfLadingValid isBillOfLadingValid)
    {
        var isValid = await isBillOfLadingValid(billOfLadingId);
        if (!isValid)
            throw new DomainException("Bill Of Lading not valid"); // not found?
    }
}
