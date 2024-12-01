using LilaRent.Application.Dto;
using LilaRent.MobileApp.Entities;


namespace LilaRent.MobileApp.Services;


public class ProfileListService : IProfileService
{
	private static Profile[] _profiles =
	[
		new() { Id = 0, Name = "Андрей Андреич", Caption = "Я Андрей Андреич. ПАДИТЕ НИЦ ПЕРЕД МОЕЙ МОЩЬЮ" },
		new() { Id = 1, Name = "Ежжи", Caption = "Caption Caption", ImagePath = "sigmastare2.jpg" },
		new() { Id = 2, Name = "Дон дондон Дон", Caption = "Донннод", ImagePath = "sigmastare3.jpg" },
		new() 
		{
			Id = 99,
			Name = "Яна Иванова", 
			Caption = "Я Яна Иванова. Иванова Яна я. Яна я Иванова. Я Иванова Яна", 
			ImagePath = "narayana.png"
		}
	];

	public ProfileSummaryDto CurrentProfile { get; set; } 


    public Profile GetProfileById(long id)
	{
		return _profiles.First(p => p.Id == id);
	}

	public IEnumerable<Profile> GetProfiles()
	{
		return _profiles;
	}

	public IEnumerable<Profile> GetHistory()
	{
		return _profiles; 
	}
}
