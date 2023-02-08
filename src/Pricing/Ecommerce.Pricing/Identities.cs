using StronglyTypedIds;

namespace Ecommerce.Pricing;

[StronglyTypedId]
public partial struct PriceId { }

[StronglyTypedId]
public partial struct ProductId { }

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String)]
public partial struct Sku { }
