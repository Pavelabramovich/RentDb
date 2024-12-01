using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LilaRent.Domain.Entities;
using LilaRent.MobileApp.Entities;


namespace LilaRent.MobileApp.Services;


public class AnnouncementListService : IFakeAnnouncementsService
{
	private AnnouncementInfo[] _list;
	private readonly IProfileService _profileService;
	public AnnouncementListService(IProfileService profs)
	{
		_profileService = profs;

		var profiles = _profileService.GetProfiles();

		_list = new AnnouncementInfo[]{
			new()
			{
				Id = 0,
				MainImagePath = "stock_image1.jpg",
				Title = "Студия милого робота",
				Category = Category.RealEstate,
				Rate = 4.7f,
				Price = 13.5M,
				Description = "Милая студия милого робота, не так ли? Обращайтесь к нам если хотите приятно посидеть " +
							  "в приятной студии. ОТличное освещение, приятные люди",
				Profile = _profileService.GetProfileById(0)
			},
			new()
			{
				Id = 1,
                MainImagePath = "stock_image2.jpg",
				Title = "Студия фиолетового робота",
				Category = Category.RealEstate,
				Rate = 4.3f,
				Price = 15.5M,
				Description = "Фиоллетовая студия фиолетового робота. Если вы любитель фиолетового вам определенно стоит поработать у нас" +
							  "Фиолетового хватит на всех. Не волнуйтесь, ситуация под контролем",
				Profile = _profileService.GetProfileById(1)
			},
			new()
			{
				Id = 2,
                MainImagePath = "stock_image3.jpg",
				Category = Category.RealEstate,
				Title = "Студия СТудия",
				Rate = 3.4f,
				Price = 20.5M,
				Description = "Студия СТУдия СтУдИя студия студия студия студия студия. Такие стихи прекрасно иллюстрируют наше отношение к " +
							  "этой жизни. Ничего нет важнее студии. Студия это жизнь. Студия это любовь.",
				Profile = _profileService.GetProfileById(2)
			},
			new()
			{
				Id = 3,
                MainImagePath = "stock_image4.jpg",
				Category = Category.RealEstate,
				Title = "Живот робота",
				Rate = 2.6f,
				Price = 10.5M,
				Description = "Потрогай его. Он такой холодный. Такой твердый. Если вы понимаете о чем мы, то вам определенно стоит " +
							  "воспользоваться нашими услугами. Серьезно. Воспользуйтесь, пока не пожалеете",
				Profile = _profileService.GetProfileById(1)
			},
			new()
			{
				Id = 4,
                MainImagePath = "dotnet_bot.png",
				Category = Category.RealEstate,
				Title = "Стол за столом",
				Rate = 3.8f,
				Price = 14.7M,
				Description = "Если вы когда-либо мечтали сесть на стол за столом, то это объявление для вас. Столы издревле служили людям," +
							  "еще гай юлий цезарь в своем письме времен его похода в галию писал: если бы не столы, то это " +
							  "нелюди давно уничтожили все что нам дорого",
				Profile = _profileService.GetProfileById(0)

			},
			new()
			{
				Id = 5,
                MainImagePath = "stock_image2.jpg",
				Title = "Тело милого робота",
				Category = Category.RealEstate,
				Rate = 4.9f,
				Price = 30.4M,
				Description = "МММдааа поторогай его вот так. Наша комания не несет ответственность за любые увечия, полученные вам во время " +
							  "оказания наших услуг. Но оно того стоит, поверьте",
				Profile = _profileService.GetProfileById(2)
			},
			new()
			{
				Id = 6,
                MainImagePath = "stock_image3.jpg",
				Title = "Стол для покера",
				Category = Category.RealEstate,
				Rate = 4.9f,
				Price = 30.4M,
				Description = "Поиграй на нем в покер. Игра мужчин. Игра шанса. Игра навыка. Человек определяется тем, когда он готов рискнуть " +
							  "всем, а когда решает подождать. Узнай чего ты стоишь",
				Profile = _profileService.GetProfileById(2)
			},
			new()
			{
				Id = 7,
                MainImagePath = "sigmastare1.jpg",
				Title = "Стол для покера",
				Category = Category.RealEstate,
				Rate = 4.9f,
				Price = 30.4M,
				Description = "Поиграй на нем в покер. Поооооокер. Рекоп. Кчау. Мы встаем из-за столика в самый нужный момент. У бармена " +
							  "лицо слоника, падает раненый клиент. Падает всяко разное запах сырников запах вина. По жизни так все завязано" +
							  " и по жизни иначе нельзя",
				Profile = _profileService.GetProfileById(2)
			},
		};
	}
	// private static Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<AnnouncementInfo, AnnouncementBasics>()));
	AnnouncementBasics toBasics(AnnouncementInfo announcement)
	{
		var res = new AnnouncementBasics();
		res.Category = announcement.Category;
		res.Rate = announcement.Rate;
		res.Price = announcement.Price;
		res.Title = announcement.Title;
		res.Id = announcement.Id;
		res.IsShortTerm = announcement.IsShortTerm;
		res.MainImagePath = announcement.MainImagePath;
		res.Description = announcement.Description;

		return res;
	}

	public async Task<IEnumerable<AnnouncementInfo>> GetAnnouncementsAsync(Func<AnnouncementInfo, bool> predicate = null)
	{
		if(predicate != null)
			return _list.Where(predicate).ToArray();

		return _list.ToArray();
	}

	public async Task<IEnumerable<AnnouncementBasics>> GetRecommendationsAsync()
	{
		return _list.Select(toBasics);
	}

	public AnnouncementInfo GetAnnouncementById(long id)
	{
		return _list[id];
	}

	public async Task<IEnumerable<AnnouncementInfo>> GetAnnouncementsBasicsFilteredAsync(Func<AnnouncementInfo, bool> filters)
	{
		return await GetAnnouncementsAsync(filters);
	}

	public async Task<IEnumerable<AnnouncementInfo>> GetAnnouncements(Func<AnnouncementInfo, bool> predicate)
	{
		if (predicate == null)
			return _list;

		return _list.Where(predicate);
	}

	public async Task<IEnumerable<AnnouncementInfo>> GetHistoryAsync()
	{
		return _list;
	}

    public async Task AddAsync(Announcement announcement)
    {
		await Task.Delay(2_000);
    }
}
