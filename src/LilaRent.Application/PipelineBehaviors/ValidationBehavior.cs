using FluentValidation;
using MediatR;


namespace LilaRent.Application.PipelineBehaviors;


internal abstract class ValidationBehavior<TRequest, TRequestProperty>(IValidator<TRequestProperty> validator)
    : ValidationBehavior<TRequest, TRequestProperty, Unit>(validator) where TRequest : notnull;


internal abstract class ValidationBehavior<TRequest, TRequestProperty, TResponce> 
    : IPipelineBehavior<TRequest, TResponce> where TRequest : notnull
{
    private readonly IValidator<TRequestProperty> _validator;


    public ValidationBehavior(IValidator<TRequestProperty> validator)
    {
        _validator = validator;
    }


    protected abstract Func<TRequest, TRequestProperty> GetRequestPropery { get; }


    public Task<TResponce> Handle(TRequest request, RequestHandlerDelegate<TResponce> next, CancellationToken cancellationToken)
    {
        var errors = _validator.Validate(GetRequestPropery(request)).Errors;

        if (errors.Any())
            throw new ValidationException(errors);

        return next();
    }
}
