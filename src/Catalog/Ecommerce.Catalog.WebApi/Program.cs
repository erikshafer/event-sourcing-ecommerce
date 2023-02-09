using Ecommerce.Catalog.Products;
using JasperFx.Core;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Marten.Exceptions;
using Npgsql;
using Oakton;
using Oakton.Resources;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Host.UseWolverine(opts =>
{
    opts.PublishMessage<ProductDrafted>()
        .ToLocalQueue("product")
        .UseDurableInbox();

    opts.PublishMessage<BrandEstablished>()
        .ToLocalQueue("product")
        .UseDurableInbox();

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
builder.Services.AddResourceSetupOnStartup();

builder.Services.AddMarten(opts =>
    {
        var connString = builder
            .Configuration
            .GetConnectionString("EventStore");

        opts.Connection(connString!);

        // opts.Projections.Add<ProductProjection1>(ProjectionLifecycle.Async);
        // opts.Projections.Add<ProductProjection2>(ProjectionLifecycle.Inline);
        opts.Projections.SelfAggregate<Product>(ProjectionLifecycle.Async);
    })
    .AddAsyncDaemon(DaemonMode.HotCold)
    .IntegrateWithWolverine()
    .EventForwardingToWolverine();

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
