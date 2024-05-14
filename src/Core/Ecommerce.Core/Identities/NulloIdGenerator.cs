namespace Ecommerce.Core.Identities;

public class NulloIdGenerator : IIdGenerator
{
    public string New() => Guid.NewGuid().ToString();
}
