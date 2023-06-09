using FluentScheduler;
using Microsoft.Extensions.Logging;

namespace Consid.Logger.Adapter.Crone.Job.Reddit;

public class GetRedditAppsDataJob : IJob
{
    private readonly ILogger _logger;

    public GetRedditAppsDataJob(ILogger logger)
    {
        _logger = logger;
    }

    public async void Execute()
    {
        await DoSomethingAsync();
    }

    private async Task DoSomethingAsync()
    {
        _logger.LogInformation(" ----->   mam");
    }
}