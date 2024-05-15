﻿using Ecommerce.Eventuous.Exceptions;
using Eventuous;
using static Catalog.Products.ProductEvents;
using static Catalog.Products.Services;

namespace Catalog.Products;

public class Product : Aggregate<ProductState>
{
    public async Task Draft(
        string productId,
        string sku,
        string name,
        string description,
        DateTimeOffset createdAt,
        string createdBy,
        IsSkuAvailable isSkuAvailable,
        IsUserAuthorized isUserAuthorized)
    {
        EnsureDoesntExist();
        await ValidateSkuAvailability(new Sku(sku), isSkuAvailable);
        await AuthorizeInternalUser(new InternalUserId(createdBy), isUserAuthorized);

        Apply(
            new V1.ProductDrafted(
                productId,
                sku,
                name,
                description,
                createdAt,
                createdBy
            )
        );
    }

    public void Activate(DateTimeOffset activatedAt, string activatedBy)
    {
        EnsureExists();

        Apply(
            new V1.ProductActivated(
                State.Id.Value,
                activatedAt,
                activatedBy
            )
        );
    }

    public void Archive(DateTimeOffset archivedAt, string archivedBy, string reason)
    {
        EnsureExists();

        Apply(
            new V1.ProductArchived(
                State.Id.Value,
                archivedAt,
                archivedBy,
                reason
            )
        );
    }

    public void CancelDraft(DateTimeOffset cancelledAt, string cancelledBy, string reason)
    {
        EnsureExists();

        Apply(
            new V1.ProductDraftCancelled(
                State.Id.Value,
                cancelledAt,
                cancelledBy,
                reason
            )
        );
    }

    public void AdjustDescription(string description, DateTimeOffset adjustedAt, string adjustedBy)
    {
        EnsureExists();

        Apply(
            new V1.ProductDescriptionAdjusted(
                State.Id.Value,
                description,
                adjustedAt,
                adjustedBy
            )
        );
    }

    public void AdjustName(string name, DateTimeOffset adjustedAt, string adjustedBy)
    {
        EnsureExists();

        Apply(
            new V1.ProductNameAdjusted(
                State.Id.Value,
                name,
                adjustedAt,
                adjustedBy
            )
        );
    }

    private static async Task ValidateSkuAvailability(Sku sku, IsSkuAvailable isSkuAvailable)
    {
        var skuAvailable = await isSkuAvailable(sku);
        if (skuAvailable is false)
            throw new DomainException("SKU not available for use");
    }

    private static async Task AuthorizeInternalUser(InternalUserId internalUserId, IsUserAuthorized isUserAuthorized)
    {
        var isValid = await isUserAuthorized(internalUserId);
        if (internalUserId.Value.Equals("robot", StringComparison.InvariantCultureIgnoreCase))
            throw new DomainException("Robots are not authorized to create products!!!");
        if (isValid is false)
            throw new DomainException("User not authorized to create product");
    }
}
