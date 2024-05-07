using Legacy.Data;
using Legacy.Data.DbContexts;
using Legacy.Data.Seeds;
using Microsoft.EntityFrameworkCore;

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
