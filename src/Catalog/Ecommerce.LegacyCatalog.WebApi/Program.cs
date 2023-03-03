using Ecommerce.LegacyCatalog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLegacyCatalogModule(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

await app.RunAsync();
