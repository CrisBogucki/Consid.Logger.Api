using Azure;
using Consid.Logger.Domain.Entity;

namespace Consid.Logger.Persist.AzureStorageTable.Model;

public class RedditLogTableEntity : RedditLogEntity, Azure.Data.Tables.ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public RedditLogTableEntity()
    {
        PartitionKey = Id.ToString();
        RowKey = Id.ToString();
        Timestamp = SetKind(DateTime.UtcNow, DateTimeKind.Utc);
    }
    
    public static DateTime SetKind(DateTime dT, DateTimeKind dTKind)
    {        
        var newDt = new DateTime(dT.Year, dT.Month, dT.Day, dT.Hour, dT.Minute, dT.Second, dT.Millisecond, dTKind);
        return newDt;
    }
    
}