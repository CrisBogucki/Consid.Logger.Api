using AutoMapper;
using Consid.Logger.Api.Dto.RedditLog;
using Consid.Logger.Domain.Event.Query.RedditLog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Consid.Logger.Api.Controller;

[ApiController]
[Route("api/reddit")]
public class RedditLogController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public RedditLogController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get logs
    /// </summary>
    [HttpGet("logs")]
    public async Task<List<RedditLogDto>> GetLogsAsync([FromQuery]GetRedditLogsRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetRedditLogsQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        return result.Select(x=> _mapper.Map<RedditLogDto>(x)).ToList();
    }

    /// <summary>
    /// Get log by id
    /// </summary>
    [HttpGet("log")]
    public async Task<RedditLogDto> GetLogAsync([FromQuery]GetRedditLogRequest request,CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetRedditLogQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        return result != null ? _mapper.Map<RedditLogDto>(result) : null;
    }
}