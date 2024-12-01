using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Services.Repositories
{
	internal interface IRepository<T>
	{
		T Create(T obj);
		IEnumerable<T> GetAll();

		T GetById(int id);

		T Update(int id, T obj);

		T Delete(int id);
	}
}
