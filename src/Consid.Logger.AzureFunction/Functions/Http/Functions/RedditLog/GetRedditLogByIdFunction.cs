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

public class GetRedditLogByIdFunction
{
    private const string Route = "log";
    
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetRedditLogByIdFunction(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [Function("GetLog")]
    [OpenApiOperation(operationId: Route, tags: nameof(SwaggerTags.Logs))]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "Id as GUID")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(RedditLogDto), Description = "Success")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Route )] 
        HttpRequestData request)
    {
        var queries = request.Queries();
        string paramId = queries["id"];
        
        if (!Guid.TryParse(paramId, out var id))
        {
            return new BadRequestObjectResult("Invalid param format. Please use valid id.");
        }

        var query = new GetRedditLogQuery(id);
        var queryResult = await _mediator.Send(query);
        if(queryResult is null) 
            return new NotFoundResult();

        var result = _mapper.Map<RedditLogDto>(queryResult);
        return new OkObjectResult(result);
    }
}