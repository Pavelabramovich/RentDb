using LilaRent.Application.Dto;


namespace LilaRent.Requests.Services;


public interface IAnnouncementService
{
    Task<IEnumerable<AnnouncementSummaryDto>> GetAllAsync();
    Task<AnnouncementUpdatingDetailsDto> GetByIdAsync(Guid id);
    Task<AnnouncementDetailsDto> GetById2Async(Guid id);
    Task<IEnumerable<AnnouncementSummaryDto>> GetByProfileIdAsync(Guid profileId, string accessToken);
    Task<IEnumerable<ReservationSummaryDto>> GetReservations(Guid announcementId);

    Task AddAsync(AnnouncementCreatingDto announcementDto);
    Task UpdateAsync(AnnouncementUpdatingDto announcementDto);
    Task DeleteAsync(Guid id);

    Task AddReservationAsync(ReservationCreatingDto reservationDto);
}
