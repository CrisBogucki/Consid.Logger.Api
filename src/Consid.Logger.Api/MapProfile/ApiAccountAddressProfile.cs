using AutoMapper;
using Consid.Logger.Api.Dto.Log;
using Consid.Logger.Domain.Event.Query.Log;

namespace Consid.Logger.Api.MapProfile;

/// <summary>
/// Api log profile
/// </summary>
public class ApiLogProfile : Profile
{
    /// <summary>
    /// Api log profile
    /// </summary>
    public ApiLogProfile()
    {
        // Request => Query
        CreateMap<GetLogRequest, GetLogQuery>();
        CreateMap<GetLogsRequest, GetLogsQuery>();

        // Request => Command

        // QueryResult => Dto
        CreateMap<GetLogQueryResult, LogDto>();
    }
}