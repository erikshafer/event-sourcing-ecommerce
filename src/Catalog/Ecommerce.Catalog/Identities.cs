using StronglyTypedIds;

namespace Ecommerce.Catalog;

#region Products

[StronglyTypedId]
public partial struct ProductId { }

#endregion

#region Skus

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String)]
public partial struct Sku { }

#endregion

#region Brands

public record Brand(BrandId BrandId, string Name)
{
    public Brand(long brandId, string name) : this(new BrandId(brandId), name) { }
}

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long)]
public partial struct BrandId { }

#endregion
