using Consid.Logger.Adapter.ExternalSource.Configuration;
using Consid.Logger.Application.Configuration;
using Consid.Logger.AzureFunction.Extensions;
using Consid.Logger.Persist.AzureStorageTable.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Consid.Logger.AzureFunction.Configuration;

public static class ExtensionsConfiguration
{
    public static IHostBuilder AddConfigureExtensions(this IHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAdapterExternalSourceConfiguration();
            services.AddRepositoryAzureStorageTableConfiguration();
            services.AddApplicationConfiguration();
            services.AddValidation();
            services.AddApiConfigure();
            services.AddOpenApiConfiguration();
            services.AddHealthChecks();
        });
        
        return builder;
    }
}