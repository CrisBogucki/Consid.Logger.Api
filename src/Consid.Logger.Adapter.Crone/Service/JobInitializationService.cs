using Consid.Logger.Adapter.Crone.Registry;
using FluentScheduler;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Consid.Logger.Adapter.Crone.Service;

public class JobInitializationService : IHostedService
{

    private readonly ILogger<JobInitializationService> _logger;

    public JobInitializationService(ILogger<JobInitializationService> logger)
    {
        _logger = logger;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        JobManager.Initialize(new SchedulerRegistry(_logger));
        _logger.LogInformation("---> Crone adapter started! ");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("---> Crone adapter stoped!");
        JobManager.Stop();
        return Task.CompletedTask;
    }
}