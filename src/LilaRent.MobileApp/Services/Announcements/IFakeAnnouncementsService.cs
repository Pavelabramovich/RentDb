using LilaRent.Domain.Entities;
using LilaRent.MobileApp.Entities;


namespace LilaRent.MobileApp.Services;


public interface IFakeAnnouncementsService
{
	public Task<IEnumerable<AnnouncementInfo>> GetAnnouncementsAsync(Func<AnnouncementInfo, bool> predicate = null);

	public Task<IEnumerable<AnnouncementInfo>> GetAnnouncementsBasicsFilteredAsync(Func<AnnouncementInfo, bool> filters);

	public Task<IEnumerable<AnnouncementBasics>> GetRecommendationsAsync();

	public AnnouncementInfo GetAnnouncementById(long id);

	public Task<IEnumerable<AnnouncementInfo>> GetAnnouncements(Func<AnnouncementInfo, bool> predicate);

	public Task<IEnumerable<AnnouncementInfo>> GetHistoryAsync();


	public Task AddAsync(Announcement announcement);
}
