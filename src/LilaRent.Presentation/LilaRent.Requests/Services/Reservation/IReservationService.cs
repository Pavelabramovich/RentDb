using LilaRent.Application.Dto;


namespace LilaRent.Requests.Services;


public interface IReservationService
{
    Task<IEnumerable<AnnouncementSummaryDto>> GetPrevios(Guid id, CancellationToken cancellationToken = default);
}
