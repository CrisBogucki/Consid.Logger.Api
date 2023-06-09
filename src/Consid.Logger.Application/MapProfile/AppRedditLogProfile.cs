using AutoMapper;
using Consid.Logger.Domain.Entity;
using Consid.Logger.Domain.Event.Command.RedditLog;
using Consid.Logger.Domain.Event.Query.RedditLog;
using Consid.Logger.Domain.Service.ExternalSource.Model;

namespace Consid.Logger.Application.MapProfile;

public class AppRedditLogProfile : Profile
{
    public AppRedditLogProfile()
    {
        // Command => Entity
        CreateMap<AddRedditLogCommand, RedditLogEntity>();
        
        // Entity => Command result
        CreateMap<RedditLogEntity, GetRedditLogQueryResult>();
        
        // Other
        CreateMap<RedditLogModel, AddRedditLogCommand>();
    }
}