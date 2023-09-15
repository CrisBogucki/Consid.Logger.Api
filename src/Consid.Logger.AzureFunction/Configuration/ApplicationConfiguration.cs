using Consid.Logger.Domain.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Consid.Logger.AzureFunction.Configuration;

public static class ApplicationConfiguration
{
    public static IHostBuilder AddAppConfiguration(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app.settings.json", false, true)
                .AddConfiguration(context.Configuration)
                .AddEnvironmentVariables();
        });
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("app.settings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var appConfig = new AppConfig();
        configuration.Bind(appConfig);

        builder.ConfigureServices(services =>
        {
            services.AddSingleton(appConfig);
        });
        
        return builder;
        
    }
}