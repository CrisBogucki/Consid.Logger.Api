using AutoMapper;
using Consid.Logger.AzureFunction.Functions.Http.Dto.RedditLog;
using Consid.Logger.Domain.Event.Query.RedditLog;

namespace Consid.Logger.AzureFunction.Functions.Http.MapProfiles;

public class ApiRedditLogProfile : Profile
{
    public ApiRedditLogProfile()
    {
        // QueryResult => Dto
        CreateMap<GetRedditLogQueryResult, RedditLogDto>();
    }
}