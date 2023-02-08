using Ecommerce.Catalog.Products;
using JasperFx.Core;
using Marten.Exceptions;
using Npgsql;
using Oakton;
using Wolverine;
using Wolverine.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Host.UseWolverine(opts =>
{
    // opts.PublishMessage<ProductReadyToBeSold>()
    //     .ToLocalQueue("product")
    //     .UseDurableInbox();

    opts.Handlers
        .OnException<ConcurrencyException>()
        .RetryTimes(3);

    opts.Handlers
        .OnException<NpgsqlException>()
        .RetryWithCooldown(50.Milliseconds(), 100.Milliseconds(), 250.Milliseconds());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/", () => "Hello World!");

await app.RunOaktonCommands(args);
