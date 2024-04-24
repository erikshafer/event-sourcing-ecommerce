using Legacy.Data;
using Legacy.Data.DbContexts;
using Legacy.Data.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLegacyData(builder.Configuration)
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

app.MapGet("/", () => "Hello World!");

if(app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var catalog = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    catalog.Database.EnsureCreated();
    catalog.Seed();
}



await app.RunAsync();

public partial class Program
{
}
