using JasperFx.Core;
using Marten.Exceptions;
using Npgsql;
using Oakton;
using Oakton.Resources;
using Products;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

builder.Services
    .AddProductsModule(builder.Configuration);

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(DraftProductHandler).Assembly);

    opts.UseFluentValidation();

    opts.Policies.AutoApplyTransactions();
    opts.Policies.UseDurableLocalQueues();

    // Opt into having Wolverine add a log message at the beginning of the message execution
    opts.Policies.LogMessageStarting(LogLevel.Information);

    // Retry when Marten encounters a concurrency exception
    opts.OnException<ConcurrencyException>()
        .RetryTimes(3);

    // Retry when encountering a Postgres exceptions
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
