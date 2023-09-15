using MediatR;

namespace Consid.Logger.Domain.Event.Query.RedditLog;

public class GetRedditLogsQuery : IRequest<IEnumerable<GetRedditLogQueryResult>>
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    
    public GetRedditLogsQuery()
    {
        
    }

    public GetRedditLogsQuery(DateTime dateFrom, DateTime dateTo)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
}