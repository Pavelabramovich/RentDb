using LilaRent.Application.Dto;
using LilaRent.Domain.Fields;


namespace LilaRent.Requests.Services;


public interface IUserService
{
    Task AddWithProfileAsync(UserWithProfileCreatingDto announcementDto);
    Task<ProfileSummaryDto> GetFirstProfileIdAsycn(string login, CancellationToken cancellationToken = default);
    Task<LegalPersonProfileDto> GetLegalPersonProfile(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ReservationSummaryDto>> GetProfileReservations(Guid profileId, LegalEntityType legalEntityType, CancellationToken cancellationToken = default);
}
