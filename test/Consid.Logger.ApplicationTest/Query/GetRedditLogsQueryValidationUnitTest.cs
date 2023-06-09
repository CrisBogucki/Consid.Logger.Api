using Consid.Logger.Application.Event.Query.RedditLog;
using Consid.Logger.Domain.Event.Query.RedditLog;
using FluentValidation.TestHelper;
using Xunit;

namespace Consid.Logger.ApplicationTest.Query;

public class GetRedditLogsQueryValidationUnitTest
{
    [Fact]
    public void Validate_ValidQuery_ShouldNotHaveValidationError()
    {
        // Arrange
        var validator = new GetRedditLogsQueryValidation();
        var query = new GetRedditLogsQuery { DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(1) };

        // Act
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.DateFrom);
        result.ShouldNotHaveValidationErrorFor(x => x.DateTo);
    }

    [Fact]
    public void Validate_EmptyDateFrom_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new GetRedditLogsQueryValidation();
        var query = new GetRedditLogsQuery { DateFrom = DateTime.MinValue, DateTo = DateTime.Now.AddDays(1) };

        // Act
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DateFrom);
        result.ShouldNotHaveValidationErrorFor(x => x.DateTo);
    }

    [Fact]
    public void Validate_EmptyDateTo_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new GetRedditLogsQueryValidation();
        var query = new GetRedditLogsQuery { DateFrom = DateTime.Now, DateTo = DateTime.MinValue };

        // Act
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.DateFrom);
        result.ShouldHaveValidationErrorFor(x => x.DateTo);
    }

    [Fact]
    public void Validate_DateFromGreaterThanDateTo_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new GetRedditLogsQueryValidation();
        var query = new GetRedditLogsQuery { DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(-1) };

        // Act
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.DateFrom);
        result.ShouldHaveValidationErrorFor(x => x.DateTo);
    }
}