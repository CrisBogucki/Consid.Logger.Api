using AutoMapper;
using Consid.Logger.Domain.Event.Command.RedditLog;
using Consid.Logger.Domain.Service.ExternalSource;
using FluentScheduler;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consid.Logger.Adapter.Crone.Job.Reddit;

public class GetRedditAppsDataJob : IJob
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IRedditLogExternalSourceService _redditLogExternalSourceService;
    
    public GetRedditAppsDataJob(IMediator mediator, ILogger logger, IRedditLogExternalSourceService redditLogExternalSourceService, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _redditLogExternalSourceService = redditLogExternalSourceService;
        _mapper = mapper;
    }

    public async void Execute()
    {
        var jobName = typeof(GetRedditAppsDataJob);   
        _logger.LogInformation(" -----> {JobName} is running", jobName);
        
        var logs = await _redditLogExternalSourceService.GetRedditLogsAsync();
        foreach (var log in logs)
        {
            var command = _mapper.Map<AddRedditLogCommand>(log);
            await _mediator.Send(command);    
        }
    }
}