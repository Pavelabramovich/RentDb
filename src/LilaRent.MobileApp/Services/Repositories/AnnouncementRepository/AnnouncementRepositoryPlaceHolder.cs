using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LilaRent.MobileApp.Services.Repositories;
using LilaRent.MobileApp.Entities;
using System.Linq.Expressions;

namespace LilaRent.MobileApp.Services.Repositories.AnnouncementRepository
{
	public class AnnouncementRepositoryPlaceHolder : IFilterableRepository<AnnouncementInfo>
	{
		private AnnouncementInfo[] _list = {
			new()
			{
				MainImagePath="dotnet_bot.png",
				Title="Студия милого робота",
				Category=Category.RealEstate,
				Rate=4.7f,
				Price=13.5M
			},
			new()
			{
				MainImagePath="dotnet_bot.png",
				Title="Студия фиолетового робота",
				Category=Category.RealEstate,
				Rate=4.3f,
				Price=15.5M
			},
			new()
			{
				MainImagePath="dotnet_bot.png",
				Category=Category.RealEstate,
				Title="Студия СТудия",
				Rate=3.4f,
				Price=20.5M
			},
			new()
			{
				MainImagePath="dotnet_bot.png",
				Category=Category.RealEstate,
				Title="Живот робота",
				Rate=2.6f,
				Price=10.5M
			},
			new()
			{
				MainImagePath="dotnet_bot.png",
				Category=Category.RealEstate,
				Title="Стол за столом",
				Rate=3.8f,
				Price=14.7M
			},
		};

		//private void FilterToParameter<ParameterType>(ref IEnumerable<AnnouncementBasics> array, 
		//				IDictionary<string, object> parameters, string key,
		//				Func<ParameterType, Func<AnnouncementBasics, bool>> predicateGenerator)
		//{
		//	if (!parameters.ContainsKey(key)) return;

		//	if (!(parameters[key] is ParameterType)) 
		//		throw new ArgumentNullException($"Filter parameter {key} is not a {typeof(ParameterType).Name} value");

		//	ParameterType value = (ParameterType) parameters[key];

		//	array = array.Where(predicateGenerator(value));
		//}

		//private IEnumerable<AnnouncementBasics> FilterAll(IEnumerable<AnnouncementBasics> arr, IDictionary<string, object> @params)
		//{
		//	FilterToParameter<decimal>(ref arr, @params, "minPrice", (value) => { return (announcement) => announcement.Price >= value; });
		//	FilterToParameter<decimal>(ref arr, @params, "maxPrice", (value) => { return (announcement) => announcement.Price <= value; });

		//	FilterToParameter<bool>(ref arr, @params, "isShortTerm", (value) => { return (announcement) => announcement.IsShortTerm == value; });

		//	return arr;
		//}

		//private IEnumerable<AnnouncementBasics> FilterRealEstate(IEnumerable<AnnouncementBasics> arr, Dictionary<string, object> @params)
		//{
		//	//arr = arr.Where(announcement => announcement.Category == Category.RealEstate);

		//	//var collection = (IEnumerable<>)
		//	throw new NotImplementedException();
		//}

		public AnnouncementInfo Create(AnnouncementInfo obj)
		{
			throw new NotImplementedException();
		}

		public AnnouncementInfo Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<AnnouncementInfo> GetAll()
		{
			throw new NotImplementedException();
		}

		public AnnouncementInfo GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<AnnouncementInfo> GetFiltered(Func<AnnouncementInfo, bool> filter)
		{
			return _list.Where(filter);
		}

		public AnnouncementInfo Update(int id, AnnouncementInfo obj)
		{
			throw new NotImplementedException();
		}
	}
}
