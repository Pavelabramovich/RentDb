using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetAnnouncementReservationQuery(Guid AnnouncementId) : IRequest<IEnumerable<ReservationSummaryDto>>;
