using FluentValidation;
using LilaRent.Application.Dto;
using LilaRent.Application.PipelineBehaviors;


namespace LilaRent.Application.UseCases.Commands;


internal class CreateUserWithProfileValidation(IValidator<UserWithProfileCreatingDto> validator) 
    : ValidationBehavior<CreateUserWithProfileCommand, UserWithProfileCreatingDto>(validator)
{
    protected override Func<CreateUserWithProfileCommand, UserWithProfileCreatingDto> GetRequestPropery => (request => request.Dto);
}
