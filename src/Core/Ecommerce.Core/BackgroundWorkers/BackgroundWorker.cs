using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Core.BackgroundWorkers;

public class BackgroundWorker : BackgroundService
{
    private readonly ILogger<BackgroundWorker> _logger;
    private readonly Func<CancellationToken, Task> _perform;

    public BackgroundWorker(
        ILogger<BackgroundWorker> logger,
        Func<CancellationToken, Task> perform)
    {
        _logger = logger;
        _perform = perform;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            await Task.Yield();
            _logger.LogInformation("Background worker started");
            await _perform(stoppingToken).ConfigureAwait(false);
            _logger.LogInformation("Background worker stopped");
        }, stoppingToken);
    }
}
