using Consid.Logger.Adapter.Crone.Job.Reddit;
using Microsoft.Extensions.Logging;

namespace Consid.Logger.Adapter.Crone.Registry;

public class SchedulerRegistry : FluentScheduler.Registry
{
    
    public SchedulerRegistry(ILogger logger)
    {
        Schedule(()=> new GetRedditAppsDataJob(logger))
            .ToRunOnceAt(DateTime.Now.AddSeconds(3))    // Delay startup for a while
            .AndEvery(2).Seconds();     // Interval
    }
}