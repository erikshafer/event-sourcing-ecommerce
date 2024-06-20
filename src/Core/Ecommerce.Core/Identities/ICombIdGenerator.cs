using RT.Comb;

namespace Ecommerce.Core.Identities;

public interface ICombIdGenerator : IIdGenerator
{
    Guid NewPostgreSqlComb(UnixDateTimeStrategy strategy = null!);
    Guid NewSqlServerComb(SqlDateTimeStrategy strategy = null!);
}
