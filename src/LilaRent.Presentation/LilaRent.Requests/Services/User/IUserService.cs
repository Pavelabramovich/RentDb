using LilaRent.Application.Dto;


namespace LilaRent.Requests.Services;


public interface IUserService
{
    Task AddWithProfileAsync(UserWithProfileCreatingDto announcementDto);
    Task<ProfileSummaryDto> GetFirstProfileIdAsycn(string login, CancellationToken cancellationToken = default);
    Task<LegalPersonProfileDto> GetLegalPersonProfile(Guid id, CancellationToken cancellationToken = default);
}
