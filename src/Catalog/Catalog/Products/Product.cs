﻿using Eventuous;

namespace Catalog.Products;

public class Product : Aggregate<ProductState>
{
    public async Task Draft(
        string productId,
        string sku,
        string name,
        string description,
        string brand,
        string[] measurements,
        DateTimeOffset createdAt,
        string createdBy,
        Services.IsSkuAvailable isSkuAvailable,
        Services.IsUserAuthorized isUserAuthorized)
    {
        EnsureDoesntExist();
        await ValidateSkuAvailability(new Sku(sku), isSkuAvailable);
        await AuthorizeInternalUser(new InternalUserId(createdBy), isUserAuthorized);

        Apply(
            new ProductEvents.V1.ProductDrafted(
                productId,
                sku,
                name,
                description,
                brand,
                measurements,
                createdAt,
                createdBy
            )
        );
    }

    public void Activate(DateTimeOffset activatedAt, string activatedBy)
    {
        EnsureExists();

        Apply(
            new ProductEvents.V1.ProductActivated(
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
            new ProductEvents.V1.ProductArchived(
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
            new ProductEvents.V1.ProductDraftCancelled(
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
            new ProductEvents.V1.ProductDescriptionAdjusted(
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
            new ProductEvents.V1.ProductNameAdjusted(
                State.Id.Value,
                name,
                adjustedAt,
                adjustedBy
            )
        );
    }

    public void AdjustBrand(string name, DateTimeOffset adjustedAt, string adjustedBy)
    {
        EnsureExists();

        Apply(
            new ProductEvents.V1.ProductBrandAdjusted(
                State.Id.Value,
                name,
                adjustedAt,
                adjustedBy
            )
        );
    }

    public void TakeMeasurement(MeasurementType type, string unit, string value)
    {
        EnsureExists();

        Apply(
            new ProductEvents.V1.ProductTakeMeasurement(
                State.Id.Value,
                Measurement.GetName(type),
                unit,
                value
            )
        );
    }

    public void RemoveMeasurement(MeasurementType type)
    {
        EnsureExists();

        Apply(
            new ProductEvents.V1.ProductRemoveMeasurement(
                State.Id.Value,
                Measurement.GetName(type)
            )
        );
    }

    private static async Task ValidateSkuAvailability(Sku sku, Services.IsSkuAvailable isSkuAvailable)
    {
        var skuAvailable = await isSkuAvailable(sku);
        if (skuAvailable is false)
            throw new DomainException("SKU not available for use");
    }

    private static async Task AuthorizeInternalUser(InternalUserId internalUserId, Services.IsUserAuthorized isUserAuthorized)
    {
        var isValid = await isUserAuthorized(internalUserId);
        if (internalUserId.Value.Equals("robot", StringComparison.InvariantCultureIgnoreCase))
            throw new DomainException("Robots are not authorized to create products!!!");
        if (isValid is false)
            throw new DomainException("User not authorized to create product");
    }
}
