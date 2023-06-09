using AutoMapper;
using Consid.Logger.Domain.Event.Query.RedditLog;
using Consid.Logger.Domain.Persist.Repository;
using MediatR;

namespace Consid.Logger.Application.Event.Query.RedditLog;

public class GetRedditLogQueryHandler : IRequestHandler<GetRedditLogQuery, GetRedditLogQueryResult>
{
    private readonly IRedditLogRepository _redditLogRepository;
    private readonly IMapper _mapper;
    
    public GetRedditLogQueryHandler(IRedditLogRepository redditLogRepository, IMapper mapper)
    {
        _redditLogRepository = redditLogRepository;
        _mapper = mapper;
    }

    public async Task<GetRedditLogQueryResult> Handle(GetRedditLogQuery request, CancellationToken cancellationToken)
    {
        var result = await _redditLogRepository.GetAsync(request.Id);
       
        if (result != null)
        {
            return _mapper.Map<GetRedditLogQueryResult>(result);
        }

        return null;

    }
}