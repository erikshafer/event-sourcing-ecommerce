namespace Ecommerce.Core.Ids;

public class NulloIdGenerator : IIdGenerator
{
    public Guid New() => Guid.NewGuid();
}