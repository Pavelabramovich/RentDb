using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetPreviosReservedAnnouncementsQuery(Guid IndividualId) : IRequest<IEnumerable<AnnouncementSummaryDto>>;
