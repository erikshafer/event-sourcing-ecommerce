namespace Legacy.Data.Entities;

public class Country
{
    public int Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// length of three (3)
    /// </summary>
    public string Code { get; set; }

    public bool? CanBillTo { get; set; }

    public bool? CanShipTo { get; set; }
}
