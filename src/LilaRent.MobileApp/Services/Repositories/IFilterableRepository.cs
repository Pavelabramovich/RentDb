using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.Services.Repositories;


internal interface IFilterableRepository<T> : IRepository<T>
{
	/// <summary>
	/// Return elements of collection that fits parameters
	/// </summary>
	/// <param name="parameters">Dictionary of parameters for filtration. For every T is unique. 
	/// If collection can have object of inherited class, use field type with type object (Category object per example).
	/// </param>
	/// <returns>Filtered collection</returns>
	IEnumerable<T> GetFiltered(Func<T, bool> filters);
}
