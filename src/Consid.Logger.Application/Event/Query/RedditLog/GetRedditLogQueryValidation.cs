using Consid.Logger.Domain.Event.Query.RedditLog;
using FluentValidation;

namespace Consid.Logger.Application.Event.Query.RedditLog;

public class GetRedditLogQueryValidation : AbstractValidator<GetRedditLogQuery>
{
    public GetRedditLogQueryValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
    }
}