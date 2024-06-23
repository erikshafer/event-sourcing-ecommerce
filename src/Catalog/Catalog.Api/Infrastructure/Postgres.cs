using Npgsql;

namespace Catalog.Api.Infrastructure;

public static class Postgres
{
    public static NpgsqlDataSource ConfigurePostgres(IConfiguration configuration)
    {
        var config = configuration.GetSection("Postgres").Get<PostgresSettings>();
        return new NpgsqlDataSourceBuilder(config!.ConnectionString).Build();
    }
}

public record PostgresSettings
{
    public string ConnectionString { get; init; } = null!;
    public string Schema { get; init; } = null!;
}
