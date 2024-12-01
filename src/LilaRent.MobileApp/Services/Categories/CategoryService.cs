using LilaRent.MobileApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Services
{
	public class CategoryService : ICategoryService
	{
		public IEnumerable<Category> GetAll()
		{
			return new[]
			{
				Category.RealEstate,
				Category.Coworkings,
				Category.Equipment,
				Category.Other
			};
		}
	}
}
