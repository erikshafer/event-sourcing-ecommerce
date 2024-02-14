namespace Catalog.Products;

public static class Services
{
    public delegate ValueTask<bool> IsProductSkuAvailable(Sku sku);
}
