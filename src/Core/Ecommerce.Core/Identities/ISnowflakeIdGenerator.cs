namespace Ecommerce.Core.Identities;

public interface ISnowflakeIdGenerator : IIdGenerator
{
    public List<string> ManyNew(int count);
}
