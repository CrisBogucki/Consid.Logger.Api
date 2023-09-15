using MediatR;

namespace Consid.Logger.Domain.Event.Query.RedditLog;

public class GetRedditLogQuery : IRequest<GetRedditLogQueryResult>
{
    public Guid Id { get; set; }

    public GetRedditLogQuery()
    {
    }

    public GetRedditLogQuery(Guid id)
    {
        Id = id;
    }
}