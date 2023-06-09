using Consid.Logger.Api.Configuration.AutoMapper;
using Consid.Logger.Api.Configuration.OpenApi;
using Consid.Logger.Api.Configuration.Swagger;
using Consid.Logger.Api.Configuration.Validation;
using Consid.Logger.Domain.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Consid.Logger.Api.Configuration;

public static class Configuration
{
    public static void AddApiConfiguration(this WebApplicationBuilder builder, AppConfig appConfig)
    {
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapperConfiguration();
        builder.Services.AddValidationConfiguration();

        //open-api
        builder.Services.AddJsonStringEnumConverter();

        // private
        builder.Services.AddRoutNameConfiguration();
        builder.Services.AddSwaggerInformationConfiguration();
        builder.Services.AddCorsConfiguration(appConfig);
    }
    
    public static void AddApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(o => { o.DefaultModelsExpandDepth(-1); });
        }

        app.UseCors(SwaggerConfiguration.CorsPolicyName);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}