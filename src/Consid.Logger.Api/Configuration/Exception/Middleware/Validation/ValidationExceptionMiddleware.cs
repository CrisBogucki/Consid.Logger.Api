using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Consid.Logger.Api.Configuration.Exception.Middleware.Validation;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            var errors = new Dictionary<string, object>();
            foreach (var error in ex.Errors.DistinctBy(x=>x.PropertyName))
            {
                errors.Add(
                    error.PropertyName, 
                    ex.Errors
                        .Where(x=> x.PropertyName == error.PropertyName)
                        .Select(x=>
                            new {
                                message = x.ErrorMessage,
                                code = x.ErrorCode
                            }));
            }

            await HandleExceptionAsync(httpContext, errors);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Dictionary<string, object> errors)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400;

        var result = new ErrorDetails()
        {
            Status = context.Response.StatusCode,
            TraceId = Activity.Current?.Id ?? context?.TraceIdentifier,
            Errors = errors
        }.ToString();
        
        await context.Response.WriteAsync(result);
    }
}