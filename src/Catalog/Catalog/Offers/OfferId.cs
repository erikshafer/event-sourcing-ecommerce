using Eventuous;

namespace Catalog.Offers;

public record OfferId(string Value) : Id(Value)
{
    public OfferId(string channelCode, string value) : this($"{channelCode}-{value}")
    {
    }
}
