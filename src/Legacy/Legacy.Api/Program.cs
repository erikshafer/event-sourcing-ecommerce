using Legacy.Api.Infrastructure;
using Legacy.Application;
using Legacy.Data;
using Legacy.Data.DbContexts;
using Legacy.Data.Seeds;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Logging.ConfigureLog(builder.Configuration);
builder.Host.UseSerilog();

builder.Services
    .AddLegacyData(builder.Configuration)
    .AddLegacyApplication()
    .AddSwaggerGen(options =>
    {
        options.CustomSchemaIds(type => type.ToString());
        options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
    })
    .AddEndpointsApiExplorer()
    .AddControllers();

const string serviceName = "legacy-api";

builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
        .AddConsoleExporter();
});
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter());

var app = builder.Build();

app
    .UseSwagger(opts =>
    {
        opts.RouteTemplate = "api/{documentName}/swagger.json";
    })
    .UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/api/v1/swagger.json", "Legacy API");
        opts.RoutePrefix = "api";
    })
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

// if(app.Environment.IsDevelopment())
using var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

// db.Database.Migrate(); // migrate

db.Seed(); // seed catalog data

await app.RunAsync();

public partial class Program
{
}
