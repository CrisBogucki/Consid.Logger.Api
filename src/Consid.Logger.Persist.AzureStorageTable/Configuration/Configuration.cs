using System.Reflection;
using Consid.Logger.Domain.Persist.Repository;
using Consid.Logger.Persist.AzureStorageTable.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Logger.Persist.AzureStorageTable.Configuration;

public static class Configuration
{
    public static void AddRepositoryAzureStorageTableConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<IRedditLogRepository, RedditLogAzureStorageTableRepository>();
    }
}