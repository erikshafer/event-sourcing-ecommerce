using System.Text.Json;
using Catalog.Products;
using Eventuous;
using Eventuous.Postgresql.Subscriptions;

namespace Catalog.Api;

public static class Registrations
{
    public static void AddEventuous(this IServiceCollection services, IConfiguration configuration)
    {
        DefaultEventSerializer.SetDefaultSerializer(
            new DefaultEventSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            )
        );

        // command services
        // services.AddCommandService

        // other internal services
        services.AddSingleton<Services.IsProductSkuAvailable>(id => new ValueTask<bool>(true));

        // event store related
        services
            .AddEventStoreClient(configuration["EventStore:ConnectionString"]!)
            .AddEventuousPostgres(configuration["Postgres:ConnectionString"]!, "catalog")
            .AddCheckpointStore<PostgresCheckpointStore>();


        // services.AddCommandService<BookingsCommandService, Booking>();
    }
}
