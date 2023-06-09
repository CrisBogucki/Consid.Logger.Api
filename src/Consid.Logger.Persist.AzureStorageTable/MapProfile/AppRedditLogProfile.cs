using AutoMapper;
using Consid.Logger.Domain.Entity;
using Consid.Logger.Persist.AzureStorageTable.Model;

namespace Consid.Logger.Persist.AzureStorageTable.MapProfile;

public class AzureStorageTableRedditLogProfile : Profile
{
    public AzureStorageTableRedditLogProfile()
    {
        // Entity => TableEntity
        CreateMap<RedditLogEntity, RedditLogTableEntity>();
        
        // TableEntity => Entity
        CreateMap<RedditLogTableEntity, RedditLogEntity>();
    }
}