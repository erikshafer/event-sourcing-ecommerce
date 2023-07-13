namespace Catalog.Brands;

public record BrandName(string Value)
{
    public static BrandName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        const int maxLength = 100;
        if (value.Length > maxLength)
            throw new ArgumentException($"Name exceeded max length of '{maxLength}'", nameof(maxLength));

        return new BrandName(value);
    }

    public bool Matches(BrandName name)
    {
        var lowerInvariantCurrent = Value.ToLowerInvariant();
        var lowerInvariantNew = name.Value.ToLowerInvariant();
        return string.Equals(lowerInvariantCurrent, lowerInvariantNew);
    }
}
