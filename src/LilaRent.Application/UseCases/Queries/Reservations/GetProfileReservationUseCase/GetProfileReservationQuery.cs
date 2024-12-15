using LilaRent.Application.Dto;
using LilaRent.Domain.Fields;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetProfileReservationQuery(Guid ProfileId, LegalEntityType LegalEntityType) : IRequest<IEnumerable<ReservationSummaryDto>>;
