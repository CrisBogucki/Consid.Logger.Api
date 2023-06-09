using Consid.Logger.Application.Event.Query.RedditLog;
using Consid.Logger.Domain.Event.Query.RedditLog;
using FluentValidation.TestHelper;
using Xunit;

namespace Consid.Logger.ApplicationTest.Query;

public class GetRedditLogQueryValidationUnitTest
{
    [Fact]
    public void Validate_ValidQuery_ShouldNotHaveValidationError()
    {
        // Arrange
        var validator = new GetRedditLogQueryValidation();
        var query = new GetRedditLogQuery { Id = Guid.NewGuid() };

        // Act
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_EmptyId_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new GetRedditLogQueryValidation();
        var query = new GetRedditLogQuery {  };

        // Act
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_NullId_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new GetRedditLogQueryValidation();
        var query = new GetRedditLogQuery {  };

        // Act
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}