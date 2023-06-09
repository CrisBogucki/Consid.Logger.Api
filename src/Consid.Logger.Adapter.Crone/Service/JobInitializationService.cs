using AutoMapper;
using Consid.Logger.Adapter.Crone.Registry;
using Consid.Logger.Domain.Service.ExternalSource;
using FluentScheduler;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Consid.Logger.Adapter.Crone.Service;

public class JobInitializationService : IHostedService
{
    private readonly ILogger<JobInitializationService> _logger;
    private readonly IMediator _mediator;
    private readonly IRedditLogExternalSourceService _redditLogExternalSourceService;
    private readonly IMapper _mapper;

    public JobInitializationService(IMediator mediator, ILogger<JobInitializationService> logger, IRedditLogExternalSourceService redditLogExternalSourceService, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _redditLogExternalSourceService = redditLogExternalSourceService;
        _mapper = mapper;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        JobManager.Initialize(new SchedulerRegistry(_mediator, _logger, _redditLogExternalSourceService, _mapper));
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