using System.Text.Json;
using Eventuous;
using Eventuous.EventStore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace Ecommerce.Core.Eventuous;

public static class Registrations
{
    public static void AddEventuousEventStoreDb(this IServiceCollection services, IConfiguration config)
    {
        DefaultEventSerializer.SetDefaultSerializer(
            new DefaultEventSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                    .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)));

        services.AddEventStoreClient(config["EventStore:ConnectionString"]!);
        services.AddAggregateStore<EsdbEventStore>();
    }
}
