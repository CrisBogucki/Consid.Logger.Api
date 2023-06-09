using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Logger.Api.Configuration.AutoMapper;

public static class AutoMapperConfiguration
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}