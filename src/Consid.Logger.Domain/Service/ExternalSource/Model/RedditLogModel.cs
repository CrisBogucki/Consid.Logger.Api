using Newtonsoft.Json;

namespace Consid.Logger.Domain.Service.ExternalSource.Model;

public class RedditLogModel
{
    [JsonProperty("no_of_comments")]
    public int NoOfComments { get; set; }

    [JsonProperty("sentiment")]
    public string Sentiment { get; set; }

    [JsonProperty("sentiment_score")]
    public double? SentimentScore { get; set; }

    [JsonProperty("ticker")]
    public string Ticker { get; set; }
}