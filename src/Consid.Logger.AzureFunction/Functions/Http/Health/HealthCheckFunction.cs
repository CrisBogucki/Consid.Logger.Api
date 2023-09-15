using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Consid.Logger.AzureFunction.Functions.Http.Health;

public class HealthCheckFunction
{
    private const string Route = "health";

    private readonly HealthCheckService _healthCheckService;

    public HealthCheckFunction(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [Function("GetHealthStatus")]
    [OpenApiOperation(operationId: Route, tags: nameof(SwaggerTags.Health))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Success")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Bad request")]
    public async Task<string?> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Route)] 
        HttpRequestData req)
    {
        var result = await _healthCheckService.CheckHealthAsync();
        return Enum.GetName(typeof(HealthStatus), result.Status);
    }  
}