using Consid.Logger.Domain.Configuration;
using Consid.Logger.Domain.Service.ExternalSource;
using Consid.Logger.Domain.Service.ExternalSource.Model;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Consid.Logger.Adapter.ExternalSource.Service;

public class RedditLogExternalSourceService : IRedditLogExternalSourceService
{
    private readonly AppConfig _appConfig;
    private readonly ILogger<RedditLogExternalSourceService> _logger;

    public RedditLogExternalSourceService(AppConfig appConfig, ILogger<RedditLogExternalSourceService> logger)
    {
        _appConfig = appConfig;
        _logger = logger;
    }

    public async Task<IEnumerable<RedditLogModel>> GetRedditLogsAsync()
    {
        var apiUrl = _appConfig.ExternalSources.RedisUrl;
        var response = await apiUrl.AllowAnyHttpStatus().GetStringAsync();
        
        if (string.IsNullOrEmpty(response) || !response.StartsWith("[{"))
        {
            _logger.LogError("External source reddit no found resource {ApiUrl}", apiUrl);
            return new List<RedditLogModel>();
        }

        return JsonConvert.DeserializeObject<List<RedditLogModel>>(response);
    }
}