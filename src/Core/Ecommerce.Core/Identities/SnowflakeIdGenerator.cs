using IdGen;

namespace Ecommerce.Core.Identities;

/// <summary>
/// Leverages `IdGen`, a Twitter Snowflake-alike ID generator.
/// https://github.com/RobThree/IdGen
/// </summary>
public class SnowflakeIdGenerator : ISnowflakeIdGenerator
{
    public string New()
    {
        var generator = new IdGenerator(0);
        var id = generator.CreateId(); // Example id: 862817670527975424
        return id.ToString();
    }

    public List<string> ManyNew(int count)
    {
        var ids = ManyNewLongIds(count);
        var convertedIds = ids.ToList().ConvertAll(id => id.ToString());
        return convertedIds;
    }

    private static IEnumerable<long> ManyNewLongIds(int count)
    {
        var generator = new IdGenerator(0);
        var ids = generator.Take(count);
        return ids;
    }
}
