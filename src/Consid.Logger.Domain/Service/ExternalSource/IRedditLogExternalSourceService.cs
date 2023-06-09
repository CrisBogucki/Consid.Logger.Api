using Consid.Logger.Domain.Service.ExternalSource.Model;

namespace Consid.Logger.Domain.Service.ExternalSource;

public interface IRedditLogExternalSourceService
{
    Task<IEnumerable<RedditLogModel>> GetRedditLogsAsync();
}