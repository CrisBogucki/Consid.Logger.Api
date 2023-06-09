using AutoMapper;
using Consid.Logger.Api.Dto.RedditLog;
using Consid.Logger.Domain.Event.Query.RedditLog;

namespace Consid.Logger.Api.MapProfile;

public class ApiRedditLogProfile : Profile
{
    public ApiRedditLogProfile()
    {
        // Request => Query
        CreateMap<GetRedditLogRequest, GetRedditLogQuery>();
        CreateMap<GetRedditLogsRequest, GetRedditLogsQuery>();

        // Request => Command

        // QueryResult => Dto
        CreateMap<GetRedditLogQueryResult, RedditLogDto>();
    }
}