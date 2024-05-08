namespace Catalog.Products;

public enum ProductStatus
{
    Unset = 0,
    Initialized = 1,
    Drafted = 2,
    Confirmed = 4,
    Cancelled = 8,
    Deprecated = 16,

    Closed = Cancelled | Deprecated
}
