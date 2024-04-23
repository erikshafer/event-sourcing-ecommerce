namespace Inventory.Procurements;

public static class Services
{
    public delegate ValueTask<bool> IsBillOfLadingValid(string billOfLadingId);
}
