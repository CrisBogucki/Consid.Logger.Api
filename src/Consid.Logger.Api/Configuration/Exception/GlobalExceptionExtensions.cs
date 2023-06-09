using Consid.Logger.Api.Configuration.Exception.Middleware.Map;
using Consid.Logger.Api.Configuration.Exception.Middleware.Validation;
using Microsoft.AspNetCore.Builder;

namespace Consid.Logger.Api.Configuration.Exception;

public static class GlobalExceptionConfiguration
{
    public static void AddGlobalExceptionConfiguration(this IApplicationBuilder app)
    {
        app.UseMiddleware<MapExceptionMiddleware>();
        app.UseMiddleware<ValidationExceptionMiddleware>();
    }
}