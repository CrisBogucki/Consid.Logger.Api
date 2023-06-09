using AutoMapper;
using Consid.Logger.Application.Event.Command.RedditLog;
using Consid.Logger.Domain.Entity;
using Consid.Logger.Domain.Event.Command.RedditLog;
using Consid.Logger.Domain.Persist.Repository;
using Moq;
using Xunit;

namespace Consid.Logger.ApplicationTest.Command;

public class AddRedditLogCommandHandlerUnitTest
{
    [Fact]
    public async Task Handle_ValidCommand_ReturnsEntityId()
    {
        // Arrange
        var redditLogRepositoryMock = new Mock<IRedditLogRepository>();
        var mapperMock = new Mock<IMapper>();

        var command = new AddRedditLogCommand();
        var entity = new RedditLogEntity { Id = Guid.NewGuid() };
        var expectedId = entity.Id;

        redditLogRepositoryMock.Setup(x => x.AddAsync(It.IsAny<RedditLogEntity>())).ReturnsAsync(entity);
        mapperMock.Setup(x => x.Map<RedditLogEntity>(command)).Returns(entity);

        var handler = new AddRedditLogCommandHandler(redditLogRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedId, result);
        redditLogRepositoryMock.Verify(x => x.AddAsync(It.IsAny<RedditLogEntity>()), Times.Once);
        mapperMock.Verify(x => x.Map<RedditLogEntity>(command), Times.Once);
    }
}