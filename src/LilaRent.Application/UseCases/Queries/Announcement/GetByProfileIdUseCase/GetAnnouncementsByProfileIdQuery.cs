using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetAnnouncementByProfileIdQuery(Guid UserId, Guid ProfileId) : IRequest<IEnumerable<AnnouncementSummaryDto>>;
