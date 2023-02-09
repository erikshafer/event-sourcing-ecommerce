using Ecommerce.Catalog.Products;
using JasperFx.Core;
using Marten;
using Marten.AspNetCore;
using Marten.Exceptions;
using Npgsql;
using Oakton;
using Oakton.Resources;
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

// Do all necessary database setup on startup
// builder.Services.AddResourceSetupOnStartup(); // TODO -- look into more

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/", () => "Hello World!");

// app.MapGet("/products", (IQuerySession session) => session.Query<Product>().ToListAsync());
//
// app.MapPost("/products/establish-brand", (EstablishBrand body, IMessageBus bus) => bus.InvokeAsync(body));
//
// app.MapPost("/products/list-tags", (ListTags body, IMessageBus bus) => bus.InvokeAsync(body));
//
// app.MapPost("/products/confirm", (ConfirmProduct body, IMessageBus bus) => bus.InvokeAsync(body));
//
// app.MapPost("/products/cancel", (CancelProduct body, IMessageBus bus) => bus.InvokeAsync(body));

await app.RunOaktonCommands(args);
