using Eventuous;

namespace Catalog.Prices;

public record Money
{
    public decimal Amount { get; internal init; }
    public string Currency { get; internal init; } = null!;

    internal Money() { }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Zero(string currency)
    {
        return new Money(0.0m, currency);
    }

    public bool IsSameCurrency(Money another) => Currency == another.Currency;

    public static Money operator -(Money one, Money another)
    {
        if (!one.IsSameCurrency(another))
            throw new DomainException("Cannot operate on different currencies");

        return new Money(one.Amount - another.Amount, one.Currency);
    }

    public static implicit operator decimal(Money money) => money.Amount;
}
