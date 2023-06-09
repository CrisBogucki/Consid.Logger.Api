using FluentValidation;
using MediatR;

namespace Consid.Logger.Application.Pipeline;

public class PipelineValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public PipelineValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators
            .Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors.DistinctBy(x => x.ErrorMessage))
            .Where(f => f != null)
            .Distinct()
            .ToList();

        if (failures.Count != 0) throw new ValidationException(failures);

        return await next();
    }
}