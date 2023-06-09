using Consid.Logger.Domain.Entity;

namespace Consid.Logger.Domain.Persist.Repository;

public interface IRedditLogRepository
{
    Task<RedditLogEntity> AddAsync(RedditLogEntity entity);
    Task<RedditLogEntity> GetAsync(Guid id);
    Task<IEnumerable<RedditLogEntity>> GetAsync(DateTime from, DateTime to);
}