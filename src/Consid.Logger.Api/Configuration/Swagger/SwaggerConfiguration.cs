using System.Reflection;
using Consid.Logger.Domain.Configuration;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Consid.Logger.Api.Configuration.Swagger;

public static class SwaggerConfiguration
{
    public static string CorsPolicyName => "CorsPolicy";
    
    public static void AddSwaggerInformationConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CONSID LOGGER",
                Version = "1.0",
                Description = "Rest API form CONSID LOGGER"
            });
        });
    }
    
    public static void AddRoutNameConfiguration(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });
    }
    
    public static void AddCorsConfiguration(this IServiceCollection services, AppConfig appConfig)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName,
                builder => builder
                    .WithOrigins(appConfig.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
        });
    }
}