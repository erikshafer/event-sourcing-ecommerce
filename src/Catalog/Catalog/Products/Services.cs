namespace Catalog.Products;

public static class Services
{
    public delegate ValueTask<bool> IsSkuAvailable(Sku sku);

    public delegate ValueTask<bool> IsUserAuthorized(InternalUserId internalUserId);
}
