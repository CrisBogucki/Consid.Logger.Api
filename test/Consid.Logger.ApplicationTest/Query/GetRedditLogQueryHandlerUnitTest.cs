using AutoMapper;
using Consid.Logger.Application.Event.Query.RedditLog;
using Consid.Logger.Domain.Entity;
using Consid.Logger.Domain.Event.Query.RedditLog;
using Consid.Logger.Domain.Persist.Repository;
using Moq;
using Xunit;

namespace Consid.Logger.ApplicationTest.Query;

public class GetRedditLogQueryHandlerUnitTest
{
    [Fact]
    public async Task Handle_ExistingId_ReturnsQueryResult()
    {
        // Arrange
        var redditLogRepositoryMock = new Mock<IRedditLogRepository>();
        var mapperMock = new Mock<IMapper>();

        var query = new GetRedditLogQuery { Id = Guid.NewGuid() };
        var entity = new RedditLogEntity { Id = query.Id };
        var queryResult = new GetRedditLogQueryResult { Id = query.Id };

        redditLogRepositoryMock.Setup(x => x.GetAsync(query.Id)).ReturnsAsync(entity);
        mapperMock.Setup(x => x.Map<GetRedditLogQueryResult>(entity)).Returns(queryResult);

        var handler = new GetRedditLogQueryHandler(redditLogRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(queryResult.Id, result.Id);
        redditLogRepositoryMock.Verify(x => x.GetAsync(query.Id), Times.Once);
        mapperMock.Verify(x => x.Map<GetRedditLogQueryResult>(entity), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingId_ReturnsNull()
    {
        // Arrange
        var redditLogRepositoryMock = new Mock<IRedditLogRepository>();
        var mapperMock = new Mock<IMapper>();

        var query = new GetRedditLogQuery { Id = Guid.NewGuid() };

        redditLogRepositoryMock.Setup(x => x.GetAsync(query.Id)).ReturnsAsync((RedditLogEntity)null);

        var handler = new GetRedditLogQueryHandler(redditLogRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
        redditLogRepositoryMock.Verify(x => x.GetAsync(query.Id), Times.Once);
        mapperMock.Verify(x => x.Map<GetRedditLogQueryResult>(It.IsAny<RedditLogEntity>()), Times.Never);
    }
}