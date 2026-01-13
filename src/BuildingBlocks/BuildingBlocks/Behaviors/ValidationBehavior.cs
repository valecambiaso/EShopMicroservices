using MediatR;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse> 
    (IEnumerable<IValidator<TRequest>> validators) //C# 12 primary-constructor parameter - provided by dependency injection
    : IPipelineBehavior<TRequest, TResponse> //it can run before/after a handler in the MediatR pipeline
    where TRequest : ICommand<TResponse> //This validation is only applyed to commands, not queries
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);//Creates a FluentValidation context wrapping the request so validators can validate against it

        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.Where(r => r.Errors.Any())
                                        .SelectMany(r => r.Errors)
                                        .ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next(); //Calls the command to continue the pipeline
    }
}
