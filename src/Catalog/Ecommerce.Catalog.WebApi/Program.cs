using Ecommerce.Catalog.Products;
using JasperFx.Core;
using Marten;
using Marten.Events.Projections;
using Marten.Exceptions;
using Npgsql;
using Oakton;
using Oakton.Resources;
using Weasel.Core;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Services.AddMarten(opts =>
    {
        var connString = builder.Configuration.GetConnectionString("marten");
        opts.Connection(connString!);
        opts.DatabaseSchemaName = "catalog";
        opts.UseDefaultSerialization(enumStorage: EnumStorage.AsString);
        opts.UseDefaultSerialization(collectionStorage: CollectionStorage.AsArray);
        opts.Projections.SelfAggregate<Product>(ProjectionLifecycle.Inline);
        opts.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
    })
    .IntegrateWithWolverine()
    .ApplyAllDatabaseChangesOnStartup();

builder.Host.UseWolverine(opts =>
{
    opts.Policies.Discovery(src =>
    {
        src.IncludeType<DraftProductHandler>();
    });

    opts.UseFluentValidation();

    opts.Policies.AutoApplyTransactions();
    opts.Policies.UseDurableLocalQueues();

    opts.OnException<ConcurrencyException>()
        .RetryTimes(3);

    opts.OnException<NpgsqlException>()
        .RetryWithCooldown(50.Milliseconds(), 100.Milliseconds(), 250.Milliseconds());
});

builder.Services.AddResourceSetupOnStartup();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapWolverineEndpoints();

return await app.RunOaktonCommands(args);
