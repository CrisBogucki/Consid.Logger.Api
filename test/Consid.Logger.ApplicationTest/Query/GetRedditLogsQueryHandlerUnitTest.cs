using AutoMapper;
using Consid.Logger.Application.Event.Query.RedditLog;
using Consid.Logger.Domain.Entity;
using Consid.Logger.Domain.Event.Query.RedditLog;
using Consid.Logger.Domain.Persist.Repository;
using Moq;
using Xunit;

namespace Consid.Logger.ApplicationTest.Query;

public class GetRedditLogsQueryHandlerUnitTest
{
    [Fact]
    public async Task Handle_ReturnsListOfQueryResults()
    {
        // Arrange
        var redditLogRepositoryMock = new Mock<IRedditLogRepository>();
        var mapperMock = new Mock<IMapper>();

        var query = new GetRedditLogsQuery { DateFrom = DateTime.Now, DateTo = DateTime.Now };
        var entities = new List<RedditLogEntity>
        {
            new RedditLogEntity { Id = Guid.NewGuid() },
            new RedditLogEntity { Id = Guid.NewGuid() },
            new RedditLogEntity { Id = Guid.NewGuid() }
        };
        var queryResults = entities.Select(e => new GetRedditLogQueryResult { Id = e.Id }).ToList();

        redditLogRepositoryMock.Setup(x => x.GetAsync(query.DateFrom, query.DateTo)).ReturnsAsync(entities);
        mapperMock.Setup(x => x.Map<GetRedditLogQueryResult>(It.IsAny<RedditLogEntity>()))
            .Returns((RedditLogEntity e) => new GetRedditLogQueryResult { Id = e.Id });

        var handler = new GetRedditLogsQueryHandler(redditLogRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(queryResults.Count, result.Count());
        Assert.Equal(queryResults.Select(r => r.Id), result.Select(r => r.Id));
        redditLogRepositoryMock.Verify(x => x.GetAsync(query.DateFrom, query.DateTo), Times.Once);
        mapperMock.Verify(x => x.Map<GetRedditLogQueryResult>(It.IsAny<RedditLogEntity>()),
            Times.Exactly(entities.Count));
    }
}