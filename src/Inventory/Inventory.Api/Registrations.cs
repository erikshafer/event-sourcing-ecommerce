using System.Text.Json;
using Eventuous;
using Eventuous.Postgresql.Subscriptions;
using Inventory.Inventories;

namespace Inventory.Api;

public static class Registrations
{
    public static void AddEventuous(this IServiceCollection services, IConfiguration configuration)
    {
        DefaultEventSerializer.SetDefaultSerializer(
            new DefaultEventSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
            )
        );

        // other internal services
        services.AddSingleton<Services.IsInventoryAvailableBySku>(id => new ValueTask<bool>(true));

        // event store related
        services
            .AddEventStoreClient(configuration["EventStore:ConnectionString"]!)
            .AddEventuousPostgres(configuration["Postgres:ConnectionString"]!, "catalog")
            .AddCheckpointStore<PostgresCheckpointStore>();
    }
}
