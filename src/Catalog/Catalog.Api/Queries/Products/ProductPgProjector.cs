using Catalog.Products;
using Eventuous.Postgresql.Projections;
using Npgsql;

namespace Catalog.Api.Queries.Products;

public class ProductPgProjector : PostgresProjector
{
    public ProductPgProjector(NpgsqlDataSource dataSource) : base(dataSource)
    {
        const string insertSql =
            """
            insert into catalog.product_drafts
                        (product_id, sku, brand_name, created_at)
                        values (@product_id, @sku, @brand_name, @created_at)
            """;

        On<ProductEvents.V1.ProductDrafted>(
            (connection, ctx) =>
                Project(
                    connection,
                    insertSql,
                    new NpgsqlParameter("@product_id", ctx.Stream.GetId()),
                    new NpgsqlParameter("@sku", ctx.Message.Sku),
                    new NpgsqlParameter("@brand_name", ctx.Message.Brand),
                    new NpgsqlParameter("@created_at", ctx.Message.CreatedAt)
                )
        );
    }
}
