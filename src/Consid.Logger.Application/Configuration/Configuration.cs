using Consid.Logger.Application.Pipeline;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Logger.Application.Configuration;

public static class Configuration
{
    public static void AddApplicationConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapperConfiguration();
        services.AddMemoryCache();
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        services.AddValidatorsFromAssemblyContaining(typeof(Configuration), ServiceLifetime.Transient);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineValidationBehavior<,>));
    }
}