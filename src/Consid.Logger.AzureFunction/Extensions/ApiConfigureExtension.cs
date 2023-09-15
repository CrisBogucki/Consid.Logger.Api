using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Logger.AzureFunction.Extensions;

public static class ApiConfigureExtension
{
    public static void AddApiConfigure(this IServiceCollection services)
    {
        services.Configure<JsonSerializerOptions>(options => 
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new JsonStringEnumConverter());
        });
    }
}