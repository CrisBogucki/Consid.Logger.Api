using System.Net;
using System.Text;
using Consid.Logger.AzureFunction.Functions.Http.Dto;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Consid.Logger.AzureFunction.Middleware;

public class ValidationExceptionMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        await HandleException(context: context, next: next);
    }
    
    private async Task HandleException(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AggregateException ex)
        {
            foreach (var innerException in ex.InnerExceptions)
            {
                if (innerException is ValidationException validationException)
                {
                    await HandleValidationException(context, validationException);
                }
            }
        }
    }

    private async Task HandleValidationException(FunctionContext context, ValidationException exception)
    {
        var validations = new Dictionary<string, object>();
        foreach (var err in exception.Errors.DistinctBy(x => x.PropertyName))
        {
            var errors = exception.Errors
                .Where(x => x.PropertyName == err.PropertyName)
                .Select(x =>
                {
                    var message = x.ErrorMessage;
                    var code = x.ErrorCode;
                    return new { message, code };
                });
            validations.Add(err.PropertyName, errors);
        }

        if (validations.Any())
        {
            var requestData = await context.GetHttpRequestDataAsync();
            if (requestData != null)
            {
                var response = requestData.CreateResponse();
                response.StatusCode = HttpStatusCode.BadRequest;
                var responseBody = new BadRequestDto(errors: validations, (int)response.StatusCode);
                var jsonResponseBody = JsonConvert.SerializeObject(responseBody, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                });
                var contentBytes = Encoding.UTF8.GetBytes(jsonResponseBody);
                await response.Body.WriteAsync(contentBytes);
                context.GetInvocationResult().Value = response;
            }
        }
    }
}