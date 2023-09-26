namespace Catalog;

public static class SkuService
{
    public delegate ValueTask<bool> IsSkuAvailable(Sku sku);
}
