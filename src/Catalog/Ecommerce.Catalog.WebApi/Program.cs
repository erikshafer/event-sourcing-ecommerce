using Ecommerce.Catalog.Products;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Oakton;
using Oakton.Resources;
using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Services.AddMarten(opts =>
    {
        var connString = builder.Configuration.GetConnectionString("marten");
        opts.Connection(connString!);
        opts.DatabaseSchemaName = "catalog";
        opts.Projections.SelfAggregate<Product>(ProjectionLifecycle.Async);
    })
    .AddAsyncDaemon(DaemonMode.HotCold)
    .IntegrateWithWolverine()
    .ApplyAllDatabaseChangesOnStartup()
    .EventForwardingToWolverine();

builder.Host.UseWolverine(opts =>
{
    opts.Policies.AutoApplyTransactions();
    opts.Policies.UseDurableLocalQueues();
});

// builder.Services.AddControllers();
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

//app.MapControllers();         // TODO: This isn't needed now. Confirm.
app.MapWolverineEndpoints();    // TODO: Swagger states "No operations defined in spec!" Bug? Mis-config?

return await app.RunOaktonCommands(args);
