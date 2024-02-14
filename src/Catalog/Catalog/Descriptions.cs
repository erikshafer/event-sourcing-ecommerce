using Eventuous;

namespace Catalog;

public record Descriptions
{
    public string ShortDescription { get; internal init; } = string.Empty;
    public string LongDescription { get; internal init; } = string.Empty;

    internal Descriptions() { }

    public Descriptions(string shortDescription, string longDescription)
    {
        if (string.IsNullOrWhiteSpace(shortDescription) && string.IsNullOrWhiteSpace(longDescription))
            throw new DomainException("At least one description must be provided");

        if (shortDescription.Length > 1_000)
            throw new DomainException("Short description cannot exceed 1000 characters");

        if (longDescription.Length > 65_000)
            throw new DomainException("Short description cannot exceed 65000 characters");

        ShortDescription = shortDescription;
        LongDescription = longDescription;
    }
}
