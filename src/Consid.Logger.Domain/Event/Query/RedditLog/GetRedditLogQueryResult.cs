using Consid.Logger.Domain.BaseModel.RedditLog;

namespace Consid.Logger.Domain.Event.Query.RedditLog;

public class GetRedditLogQueryResult : RedditLogBaseModel
{
    public Guid Id { get; set; }
}