using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetAnnouncementById2Query(Guid Id) : IRequest<AnnouncementDetailsDto>;
