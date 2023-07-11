namespace Catalog;

public record Sku(string Value)
{
    private Sku() : this(string.Empty)
    {
    }

    public static Sku Empty()
    {
        return new Sku();
    }
}
