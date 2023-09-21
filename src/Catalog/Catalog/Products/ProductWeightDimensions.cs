namespace Catalog.Products;

public record ProductWeightDimensions
{
    public float Weight { get; init; }
    public float Height { get; init; }
    public float Length { get; init; }
    public float Depth { get; init; }

    private ProductWeightDimensions() { }

    public ProductWeightDimensions(float weight, float height, float length, float depth)
    {
        if (weight < 0)
            throw new ArgumentOutOfRangeException(nameof(weight));
        if (height < 0)
            throw new ArgumentOutOfRangeException(nameof(height));
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length));
        if (depth < 0)
            throw new ArgumentOutOfRangeException(nameof(depth));

        Weight = weight;
        Height = height;
        Length = length;
        Depth = depth;
    }

    public void Deconstruct(out float weight, out float height, out float length, out float depth)
    {
        weight = Weight;
        height = Height;
        length = Length;
        depth = Depth;
    }

    public bool Matches(ProductWeightDimensions weightDimensions)
    {
        var (weight, height, length, depth) = weightDimensions;

        const float tolerance = 0.0001f;

        if (Math.Abs(weight - Weight) < tolerance &&
            Math.Abs(height - Height) < tolerance &&
            Math.Abs(length - Length) < tolerance &&
            Math.Abs(depth - Depth) < tolerance)
            return false;

        return true;
    }
}
