using Eventuous.Spyglass;
using Microsoft.AspNetCore.Http.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Serilog;
using ShoppingCart.Api;
using ShoppingCart.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Logging.ConfigureLog(builder.Configuration);
builder.Host.UseSerilog();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTelemetry();
builder.Services.AddEventuous(builder.Configuration);
builder.Services.AddEventuousSpyglass();

builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
);

var app = builder.Build();

app.UseSwagger(opts =>
    {
        opts.RouteTemplate = "api/{documentName}/swagger.json";
    })
    .UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/api/v1/swagger.json", "ShoppingCart API");
        opts.RoutePrefix = "api";
    });
app.UseSerilogRequestLogging();
app.MapControllers();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.MapEventuousSpyglass();

try {
    app.Run("http://*:5262");
    return 0;
}
catch (Exception e) {
    Log.Fatal(e, "Host terminated unexpectedly");
    return 1;
}
finally {
    Log.CloseAndFlush();
}
