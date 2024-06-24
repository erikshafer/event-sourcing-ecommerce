using Catalog.Api;
using Ecommerce.Core.WebApi.Infrastructure;
using Ecommerce.Core.WebApi.Swagger;
using Eventuous.Spyglass;
using Microsoft.AspNetCore.Http.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Logging.ConfigureLog(builder.Configuration);
builder.Host.UseSerilog();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(
    "EventSourcingEcommerce - Catalog HTTP API",
    "v1",
    "The Catalog service HTTP API");
builder.Services.AddTelemetry();
builder.Services.AddEventuous(builder.Configuration);
builder.Services.AddEventuousSpyglass();

builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
);

var app = builder.Build();

app.UseSwaggerAndSwaggerUI(name: "Catalog");
app.UseSerilogRequestLogging();
app.MapControllers();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.MapEventuousSpyglass();

try {
    app.Run("http://*:5252");
    return 0;
}
catch (Exception e) {
    Log.Fatal(e, "Host terminated unexpectedly");
    return 1;
}
finally {
    Log.CloseAndFlush();
}

