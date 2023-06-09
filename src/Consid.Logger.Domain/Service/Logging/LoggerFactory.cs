using Microsoft.Extensions.Configuration;
using Serilog;

namespace Consid.Logger.Domain.Service.Logging;

public static class LoggerFactory
{
    public static ILogger CreateInstance(IConfiguration configuration)
    {
        return new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateBootstrapLogger();
    }
}