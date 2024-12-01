using FluentValidation;
using LilaRent.Application.Dto;
using LilaRent.Application.PipelineBehaviors;


namespace LilaRent.Application.UseCases.Commands;


internal class CreateAnnouncementValidation(IValidator<AnnouncementCreatingDto> validator) 
    : ValidationBehavior<CreateAnnouncementCommand, AnnouncementCreatingDto>(validator)
{
    protected override Func<CreateAnnouncementCommand, AnnouncementCreatingDto> GetRequestPropery => (request => request.Dto);
}
