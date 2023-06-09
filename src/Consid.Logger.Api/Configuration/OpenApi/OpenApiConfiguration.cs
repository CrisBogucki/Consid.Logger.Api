using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Logger.Api.Configuration.OpenApi;

public static class OpenApiConfiguration
{
    public static void AddJsonStringEnumConverter(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(o
            => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }
}