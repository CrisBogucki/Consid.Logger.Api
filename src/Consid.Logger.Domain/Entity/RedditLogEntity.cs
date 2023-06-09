namespace Consid.Logger.Domain.Entity;

public class RedditLogEntity : BaseEntity
{
    public int NoOfComments { get; set; }
    public string Sentiment { get; set; }
    public double? SentimentScore { get; set; }
    public string Ticker { get; set; }
    public DateTime Created { get; set; }
}