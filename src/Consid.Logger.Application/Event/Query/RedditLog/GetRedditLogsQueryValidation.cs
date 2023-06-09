using Consid.Logger.Domain.Event.Query.RedditLog;
using FluentValidation;

namespace Consid.Logger.Application.Event.Query.RedditLog;

public class GetRedditLogsQueryValidation : AbstractValidator<GetRedditLogsQuery>
{
    public GetRedditLogsQueryValidation()
    {
        RuleFor(x => x.DateFrom)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.DateTo)
            .NotEmpty()
            .NotNull()
            .GreaterThan(x => x.DateFrom);
    }
}