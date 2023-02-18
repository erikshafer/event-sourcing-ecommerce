using Ecommerce.Catalog.Products;
using JasperFx.Core;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Marten.Exceptions;
using Npgsql;
using Oakton;
using Weasel.Core;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Http;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Services.AddMarten(opts =>
    {
        var connString = builder.Configuration.GetConnectionString("marten");
        opts.Connection(connString!);
        opts.Projections.SelfAggregate<Product>(ProjectionLifecycle.Inline);
        opts.AutoCreateSchemaObjects = AutoCreate.All;
    })
    .AddAsyncDaemon(DaemonMode.HotCold)
    .IntegrateWithWolverine()
    .ApplyAllDatabaseChangesOnStartup()
    .EventForwardingToWolverine();

builder.Host.UseWolverine(opts =>
{
    opts.Policies.AutoApplyTransactions();
    opts.Policies.UseDurableLocalQueues();

    opts.OnException<ConcurrencyException>()
        .RetryTimes(3);

    opts.OnException<NpgsqlException>()
        .RetryWithCooldown(50.Milliseconds(), 100.Milliseconds(), 250.Milliseconds());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Do all necessary database setup on startup
// builder.Services.AddResourceSetupOnStartup();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapWolverineEndpoints();

return await app.RunOaktonCommands(args);
