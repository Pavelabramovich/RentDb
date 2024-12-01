using FluentValidation;
using LilaRent.Application.Dto;
using LilaRent.Application.PipelineBehaviors;


namespace LilaRent.Application.UseCases.Commands;


internal class UpdateAnnouncementValidation(IValidator<AnnouncementUpdatingDto> validator) 
    : ValidationBehavior<UpdateAnnouncementCommand, AnnouncementUpdatingDto>(validator)
{
    protected override Func<UpdateAnnouncementCommand, AnnouncementUpdatingDto> GetRequestPropery => (request => request.Dto);
}
