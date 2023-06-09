using AutoMapper;
using Consid.Logger.Adapter.Crone.Job.Reddit;
using Consid.Logger.Domain.Service.ExternalSource;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consid.Logger.Adapter.Crone.Registry;

public class SchedulerRegistry : FluentScheduler.Registry
{
    public SchedulerRegistry(IMediator mediator, ILogger logger, IRedditLogExternalSourceService redditLogExternalSourceService, IMapper mapper )
    {
        Schedule(()=> new GetRedditAppsDataJob(mediator, logger, redditLogExternalSourceService, mapper))
            .ToRunOnceAt(DateTime.Now.AddSeconds(3))    // Delay startup for a while
            .AndEvery(1).Minutes();     // Interval
    }
}