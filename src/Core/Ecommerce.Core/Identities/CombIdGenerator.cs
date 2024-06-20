using RT.Comb;

namespace Ecommerce.Core.Identities;

/// <summary>
/// Generates a "COMB" GUID value. Comes in PostgreSQL or SQL Server flavors.
/// - SqlCombProvider: This creates and decodes COMBs in GUIDs that are compatible with the way Microsoft SQL Server sorts uniqueidentifier values -- i.e., starting at the 11th byte.
/// - PostgreSqlCombProvider: This creates and decodes COMBs in GUIDs that are compatible with the way PostgreSQL sorts uuid values -- i.e., starting with the first byte shown in string representations of a Guid.
/// If you aren't using this generator to store an ID in either database, pick your favorite flavor! I recommend PostgreSQL. =)
///
/// https://github.com/richardtallent/RT.Comb
/// </summary>
public class CombIdGenerator : ICombIdGenerator
{
    /// <summary>
    /// Leverages NewPostgreSqlComb()
    /// </summary>
    public string New()
    {
        return NewPostgreSqlComb().ToString();
    }

    public Guid NewPostgreSqlComb(UnixDateTimeStrategy strategy = null!)
    {
        strategy ??= new UnixDateTimeStrategy();

        var generator = new PostgreSqlCombProvider(strategy);
        var id = generator.Create();
        return id;
    }

    public Guid NewSqlServerComb(SqlDateTimeStrategy strategy = null!)
    {
        strategy ??= new SqlDateTimeStrategy();

        var generator = new SqlCombProvider(strategy);
        var id = generator.Create();
        return id;
    }
}
