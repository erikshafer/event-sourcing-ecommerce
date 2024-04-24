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
        var eventstore = configuration["EventStore:ConnectionString"]!;
        var postgres = configuration["Postgres:ConnectionString"]!;
        services
            .AddEventStoreClient(eventstore)
            .AddEventuousPostgres(postgres, "catalog")
            .AddCheckpointStore<PostgresCheckpointStore>();
    }
}
