using Consid.Logger.Domain.Configuration;
namespace Consid.Logger.Host;

public static class Configuration
{
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