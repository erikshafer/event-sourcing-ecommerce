namespace Catalog;

public static class Services
{
    public delegate ValueTask<bool> IsSkuAvailable(Sku sku);
}
