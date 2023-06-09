using MediatR;

namespace Consid.Logger.Domain.Event.Command.RedditLog;

public class AddRedditLogCommand: IRequest<Guid>
{
    public int NoOfComments { get; set; }
    public string Sentiment { get; set; }
    public double? SentimentScore { get; set; }
    public string Ticker { get; set; }
}