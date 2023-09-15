using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Consid.Logger.AzureFunction.Functions.WarmUp;

public static class WarmUpFunction
{
    [Function("WarmUp")]
    public static void Run([Microsoft.Azure.Functions.Worker.WarmupTrigger()] WarmupContext context, ILogger log)
    {
        log.LogInformation("Function App instance is warm 🌞🌞🌞");
    }
}