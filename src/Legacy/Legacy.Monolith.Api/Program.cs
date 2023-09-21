using Legacy.Monolith.Catalog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLegacyCatalogModule(builder.Configuration)
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app.ConfigureLegacyCatalogModule();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

// await app.Services.GetRequiredService<LegacyCatalogDbContext>().Database.EnsureCreatedAsync();

app.MapGet("/", () => "Hello World!");

await app.RunAsync();

public partial class Program
{
}
