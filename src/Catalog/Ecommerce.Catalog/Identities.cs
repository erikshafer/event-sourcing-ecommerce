using StronglyTypedIds;

namespace Ecommerce.Catalog;

[StronglyTypedId]
public partial struct ProductId { }

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int)]
public partial struct BrandId { }
