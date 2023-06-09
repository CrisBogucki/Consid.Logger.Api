using MediatR;

namespace Consid.Logger.Domain.Event.Query.RedditLog;

public class GetRedditLogsQuery : IRequest<List<GetRedditLogQueryResult>>
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}