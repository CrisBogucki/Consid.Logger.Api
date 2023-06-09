using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Logger.Api.Configuration.Validation;

public static class ValidationConfiguration
{
    public static void AddValidationConfiguration(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ValidateModelStateAttribute));
        });
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
    }
}