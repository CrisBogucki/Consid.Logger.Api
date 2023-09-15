using Microsoft.AspNetCore.Mvc;

namespace Consid.Logger.AzureFunction.Functions.Http.Dto.RedditLog;

public class GetRedditLogsRequest
{
    [FromQuery]
    public DateTime DateFrom { get; set; }
    
    [FromQuery]
    public DateTime DateTo { get; set; }
}