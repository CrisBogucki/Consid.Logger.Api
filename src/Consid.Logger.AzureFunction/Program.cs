using Consid.Logger.AzureFunction.Configuration;
using Consid.Logger.AzureFunction.Middleware;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .AddAppConfiguration()
    .ConfigureFunctionsWorkerDefaults(workerApplication =>
    {
        workerApplication.UseMiddleware<ValidationExceptionMiddleware>();
    })
    .AddConfigureExtensions()
    .Build();

await host.RunAsync();