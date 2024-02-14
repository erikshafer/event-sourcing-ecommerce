namespace Catalog.Products;

public enum ProductStatus
{
    Unset = 0,
    Initialized = 1,
    Confirmed = 2,
    Cancelled = 4,
    Deprecated = 8,

    Closed = Confirmed | Cancelled | Deprecated
}
