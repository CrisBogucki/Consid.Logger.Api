using AutoMapper;
using Consid.Logger.Api.Dto.Log;
using Consid.Logger.Domain.Event.Query.Log;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Consid.Logger.Api.Controller;

[ApiController]
[Route("api")]
public class LogController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public LogController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get logs
    /// </summary>
    [HttpGet("logs")]
    public async Task<List<LogDto>> GetLogsAsync([FromQuery]GetLogsRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetLogsQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        return result.Logs.Select(x=> _mapper.Map<LogDto>(x)).ToList();
    }

    /// <summary>
    /// Get log by id
    /// </summary>
    [HttpGet("log")]
    public async Task<LogDto> GetLogAsync([FromQuery]GetLogRequest request,CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetLogQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        return _mapper.Map<LogDto>(result);
    }
}