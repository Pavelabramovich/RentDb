using LilaRent.Application.Dto;
using LilaRent.MobileApp.Entities;


namespace LilaRent.MobileApp.Services;


public interface IProfileService
{
	IEnumerable<Profile> GetProfiles();

	IEnumerable<Profile> GetHistory();

	Profile GetProfileById(long id);

	ProfileSummaryDto CurrentProfile { get; set; }
}
