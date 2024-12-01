using LilaRent.MobileApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Services
{
    public interface ICategoryService
    {
		public IEnumerable<Category> GetAll();

		
    }
}
