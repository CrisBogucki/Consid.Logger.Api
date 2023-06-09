using Consid.Logger.Domain.Configuration;
using Serilog;
using LoggerFactory = Consid.Logger.Domain.Service.Logging.LoggerFactory;

namespace Consid.Logger.Host;

public static class Configuration
{
    public static void AddSerilogConfiguration(this WebApplicationBuilder builder)
    {
        var logger = LoggerFactory.CreateInstance(builder.Configuration);
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSerilog(logger, false);
        });
    }

    public static AppConfig AddAppConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Services.Configure<AppConfig>(provider => new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
            .Build());

        var appConfig = new AppConfig();
        builder.Configuration.Bind(appConfig);
        builder.Services.AddSingleton(appConfig);

        return appConfig;
    }
}