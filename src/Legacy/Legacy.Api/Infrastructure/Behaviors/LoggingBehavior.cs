using MediatR.Pipeline;

namespace Legacy.Api.Infrastructure.Behaviors;

public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILoggerFactory logger)
    {
        _logger = logger.CreateLogger<TRequest>();
    }

    public Task Process(TRequest request, CancellationToken ct)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Request: {Name} {@Request}", requestName, request);

        return Task.CompletedTask;
    }
}
