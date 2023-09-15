using AutoMapper;
using Consid.Logger.Domain.Event.Command.RedditLog;
using Consid.Logger.Domain.Service.ExternalSource;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Consid.Logger.AzureFunction.Functions.Timer;

public class ImportLogsTimerFunctionAsync
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IRedditLogExternalSourceService _redditLogExternalSourceService;

    public ImportLogsTimerFunctionAsync(IMediator mediator, IMapper mapper, IRedditLogExternalSourceService redditLogExternalSourceService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _redditLogExternalSourceService = redditLogExternalSourceService;
    }

    [Function("ImportLogsTimerFunction")]
    public async Task RunAsync([TimerTrigger("10 * * * * *")] TimerInfo myTimer)
    {
        var logs = await _redditLogExternalSourceService.GetRedditLogsAsync();
        foreach (var log in logs)
        {
            var command = _mapper.Map<AddRedditLogCommand>(log);
            await _mediator.Send(command);    
        }
    }
}