using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;

namespace Consid.Logger.Api.Configuration.Exception.Middleware.Map;

public class MapExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public MapExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (AutoMapperMappingException ex)
        {
            if (ex.Types != null)
            {
                var types = ex.Types.Value;

                await HandleExceptionAsync(httpContext, types);
            }
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, TypePair types)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400;

        await context.Response.WriteAsync(types.ToString());
    }
}