using Legacy.Data;

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

await app.RunAsync();

public partial class Program
{
}
