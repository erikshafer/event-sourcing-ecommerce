using Inventory.Api;
using Inventory.Api.Infrastructure;
using Microsoft.AspNetCore.Http.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Logging.ConfigureLog(builder.Configuration);
builder.Host.UseSerilog();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb));

// Adding Eventuous. This may be pushed down to the core/domain library (layer). TBD.
builder.Services.AddEventuous(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
    options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger(opts =>
        {
            opts.RouteTemplate = "api/{documentName}/swagger.json";
        })
        .UseSwaggerUI(opts =>
        {
            opts.SwaggerEndpoint("/api/v1/swagger.json", "Legacy API");
            opts.RoutePrefix = "api";
        });
}

// app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseSwagger().UseSwaggerUI();
app.MapControllers();

try {
    app.Run("http://*:7024");
    return 0;
}
catch (Exception e) {
    Log.Fatal(e, "Host terminated unexpectedly");
    return 1;
}
finally {
    Log.CloseAndFlush();
}
