using Consid.Logger.Adapter.Crone.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Consid.Logger.Adapter.Crone.Configuration;

public static class Configuration
{
    public static void AddAdapterCroneConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IHostedService, JobInitializationService>();
    }
}