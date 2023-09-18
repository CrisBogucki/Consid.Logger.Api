using System.Net;
using AutoMapper;
using Consid.Logger.AzureFunction.Functions.Http.Dto.RedditLog;
using Consid.Logger.Domain.Event.Query.RedditLog;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Consid.Logger.AzureFunction.Functions.Http.Functions.RedditLog;

public class GetRedditLogsFunction
{
    private const string Route = "logs";
    
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetRedditLogsFunction(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [Function("GetRedditLogs")]
    [OpenApiOperation(operationId: Route, tags: nameof(SwaggerTags.Logs))]
    [OpenApiParameter(name: "from", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Filter data - from")]
    [OpenApiParameter(name: "to", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Filter data - to")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<RedditLogDto>), Description = "Success")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Route )] 
        HttpRequestData request)
    {
        var queries = request.Queries();
        string paramFrom = queries["from"];
        string paramTo = queries["to"];
        
        if (!DateTime.TryParse(paramFrom, out var from) || !DateTime.TryParse(paramTo, out var to))
        {
            return new BadRequestObjectResult("Invalid date format. Please use valid DateTime format.");
        }
        
        var query = new GetRedditLogsQuery(from, to);
        var queryResult = await _mediator.Send(query);
        var dtoResult = queryResult.Select(_mapper.Map<RedditLogDto>);
        return new OkObjectResult(dtoResult);
    }
}