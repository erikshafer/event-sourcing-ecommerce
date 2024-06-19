namespace Catalog.Products;

public record Measurement
{
    public string Type { get; private set; } = default!;
    public string Unit { get; private set; } = default!;
    public string Value { get; private set; } = default!;

    public MeasurementType GetMeasurementType() => GetName(Type);

    internal Measurement() { }

    public Measurement(MeasurementType type, string unit, string value)
    {
        Type = GetName(type);

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));
        Unit = unit;

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        Value = value;
    }

    public static string GetName(MeasurementType type) => type switch
    {
        MeasurementType.Unset => throw new ArgumentException("Must set type of measurement"),
        MeasurementType.Dimension => Constants.Measurements.Types.Dimension,
        MeasurementType.Volume => Constants.Measurements.Types.Volume,
        MeasurementType.Weight => Constants.Measurements.Types.Weight,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };

    public static MeasurementType GetName(string type) => type switch
    {
        "Dimension" => MeasurementType.Dimension,
        "Volume" => MeasurementType.Volume,
        "Weight" => MeasurementType.Weight,
        _ => throw new ArgumentException("Invalid name of measurement type")
    };

    public Measurement (string type, string unit, string value)
        : this(GetName(type), unit, value)
    {
    }

    public void Deconstruct(out string type, out string unit, out string value)
    {
        type = Type;
        unit = Unit;
        value = Value;
    }

    public bool Matches(Measurement otherMeasurement) =>
        Type == otherMeasurement.Type &&
        Unit == otherMeasurement.Unit &&
        Value == otherMeasurement.Value;

    public bool MatchesTypeAndUnit(Measurement otherMeasurement) =>
        MatchesType(otherMeasurement.GetMeasurementType()) &
        MatchesUnit(otherMeasurement.Unit);

    public bool MatchesType(MeasurementType otherType) =>
        Type == GetName(otherType);

    public bool MatchesUnit(string otherUnit) =>
        Unit == otherUnit;

    public bool MatchesValue(string otherValue) =>
        Value == otherValue;
}

public enum MeasurementType
{
    Unset = 0,
    Dimension = 1,
    Volume = 2,
    Weight = 3
}

