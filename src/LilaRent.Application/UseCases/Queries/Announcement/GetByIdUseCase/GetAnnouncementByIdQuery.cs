using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetAnnouncementByIdQuery(Guid Id) : IRequest<AnnouncementUpdatingDetailsDto>;
