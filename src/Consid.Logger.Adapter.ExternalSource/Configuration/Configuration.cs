using Consid.Logger.Adapter.ExternalSource.Service;
using Consid.Logger.Domain.Service.ExternalSource;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Logger.Adapter.ExternalSource.Configuration;

public static class Configuration
{
    public static void AddAdapterExternalSourceConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IRedditLogExternalSourceService, RedditLogExternalSourceService>();
    }
}