using LilaRent.MobileApp.Entities;
using System.Linq.Expressions;


namespace LilaRent.MobileApp.Services;


public interface IOrdersService
{
	public Task<IEnumerable<OrderBasic>> GetAllAsync();

	public Task<OrderBasic> GetByIdAsync(long id);

	public Task<IEnumerable<OrderBasic>> GetWhereAsync(Func<OrderBasic, bool> predicate);

	public Task<IDictionary<string, IEnumerable<OrderBasic>>> GetOrganizedAsync();

	// как работает создание записи для другого человека
	public Task<OrderBasic> CreateOrderAsync(AnnouncementInfo announcement, DateOnly Date, TimeRange range);

	public Task CancelOrder(long id);
}
