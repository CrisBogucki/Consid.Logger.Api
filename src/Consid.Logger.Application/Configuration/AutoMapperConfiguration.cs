using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Logger.Application.Configuration;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}