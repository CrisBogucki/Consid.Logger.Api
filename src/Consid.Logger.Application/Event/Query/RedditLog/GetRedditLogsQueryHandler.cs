using AutoMapper;
using Consid.Logger.Domain.Event.Query.RedditLog;
using Consid.Logger.Domain.Persist.Repository;
using MediatR;

namespace Consid.Logger.Application.Event.Query.RedditLog;

public class GetRedditLogsQueryHandler : IRequestHandler<GetRedditLogsQuery, IEnumerable<GetRedditLogQueryResult>>
{
    private readonly IRedditLogRepository _redditLogRepository;
    private readonly IMapper _mapper;
    
    public GetRedditLogsQueryHandler(IRedditLogRepository redditLogRepository, IMapper mapper)
    {
        _redditLogRepository = redditLogRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetRedditLogQueryResult>> Handle(GetRedditLogsQuery request, CancellationToken cancellationToken)
    {
        var result = await _redditLogRepository.GetAsync(request.DateFrom, request.DateTo);
        var redditLogEntities = result?.ToList();
        return redditLogEntities?.Select(x => _mapper.Map<GetRedditLogQueryResult>(x));
    }
}