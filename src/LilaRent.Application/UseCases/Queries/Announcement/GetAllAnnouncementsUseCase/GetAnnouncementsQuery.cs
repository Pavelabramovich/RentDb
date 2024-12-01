using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetAnnouncementsQuery : IRequest<IEnumerable<AnnouncementSummaryDto>>;
