using Consid.Logger.Domain.BaseModel.RedditLog;

namespace Consid.Logger.Api.Dto.RedditLog;

public class RedditLogDto : RedditLogBaseModel
{
    public Guid Id { get; set; }
}