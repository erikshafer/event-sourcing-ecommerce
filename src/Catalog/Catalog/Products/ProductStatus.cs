namespace Catalog.Products;

public enum ProductStatus
{
    Unset = 0,
    Drafted = 1,
    Activated = 2,
    Cancelled = 8,
    Archived = 16,

    Closed = Cancelled | Archived
}
