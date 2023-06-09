using AutoMapper;
using Consid.Logger.Domain.Entity;
using Consid.Logger.Domain.Event.Command.RedditLog;
using Consid.Logger.Domain.Persist.Repository;
using MediatR;

namespace Consid.Logger.Application.Event.Command.RedditLog;

public class AddRedditLogCommandHandler : IRequestHandler<AddRedditLogCommand, Guid>
{
    private readonly IRedditLogRepository _redditLogRepository;
    private readonly IMapper _mapper;

    public AddRedditLogCommandHandler(IRedditLogRepository redditLogRepository, IMapper mapper)
    {
        _redditLogRepository = redditLogRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddRedditLogCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<RedditLogEntity>(request);
        entity.Created = DateTime.UtcNow;
        entity = await _redditLogRepository.AddAsync(entity);
        return entity.Id;
    }
}