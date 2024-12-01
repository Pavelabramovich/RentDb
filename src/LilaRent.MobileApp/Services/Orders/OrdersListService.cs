using LilaRent.MobileApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.Services;


public class OrdersListService : IOrdersService
{
    private IList<OrderBasic> _orders;

	// for user that made order
	private readonly IFakeUserService _userService;

	// for profile that publishes assignment
	private readonly IProfileService _profileService;

	private readonly IFakeAnnouncementsService _announcementsService;

	public OrdersListService(IFakeUserService userService, IProfileService profileService, IFakeAnnouncementsService announcementService)
	{
		_userService = userService;
		_announcementsService = announcementService;
		_profileService = profileService;

		_orders = new List<OrderBasic>()
		{
			new OrderBasic()
			{
				Id = 0,
				Range = new TimeRange()
				{
					Begin = TimeOnly.FromDateTime(DateTime.Now - TimeSpan.FromHours(1)),
					End = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromHours(1))
				},

				Date = DateOnly.FromDateTime(DateTime.Now),

				Announcement = _announcementsService.GetAnnouncementById(0),
				User = _userService.GetCurrentUser()
			},
			new OrderBasic()
			{
				Id = 1,
				Range = new TimeRange()
				{
					Begin = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(1)),
					End = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(1) + TimeSpan.FromHours(1)),
				},

				Date = DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(1)),

				Announcement = _announcementsService.GetAnnouncementById(1),
				User = _userService.GetCurrentUser()

			},
			new OrderBasic()
			{
				Range = new TimeRange()
				{
					Begin = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(2)),
					End = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(2) + TimeSpan.FromHours(1)),
				},

				Date = DateOnly.FromDateTime(DateTime.Now + TimeSpan.FromDays(2)),

				Announcement = _announcementsService.GetAnnouncementById(2),
				User = _userService.GetCurrentUser()
			},
			new OrderBasic()
			{
				Id = 2,
				Range = new TimeRange()
				{
					Begin = TimeOnly.FromDateTime(DateTime.Now - TimeSpan.FromDays(3)),
					End = TimeOnly.FromDateTime(DateTime.Now - TimeSpan.FromDays(3) + TimeSpan.FromHours(2)),
				},

				Date = DateOnly.FromDateTime(DateTime.Now - TimeSpan.FromDays(3)),

				Announcement = _announcementsService.GetAnnouncementById(3),
				User = _userService.GetCurrentUser()
			},

			new OrderBasic()
			{
				Id = 3,
				Range = new TimeRange()
				{
					Begin = TimeOnly.FromDateTime(DateTime.Now - TimeSpan.FromHours(1)),
					End = TimeOnly.FromDateTime(DateTime.Now + TimeSpan.FromHours(1)),
				},

				Date = DateOnly.FromDateTime(DateTime.Now),

				Announcement = _announcementsService.GetAnnouncementById(1),
				User = _userService.GetCurrentUser()
			}
		};
	}

    public Task<IEnumerable<OrderBasic>> GetAllAsync()
    {
        return Task.FromResult(_orders.AsEnumerable());
    }

    public Task<IEnumerable<OrderBasic>> GetWhereAsync(Func<OrderBasic, bool> predicate)
    {
        return Task.FromResult(_orders.Where(predicate));
    }

	public Task<IDictionary<string, IEnumerable<OrderBasic>>> GetOrganizedAsync()
	{
		var all = GetAllAsync().Result;

		Dictionary<string, IEnumerable<OrderBasic>> res = new();

		var past = new List<OrderBasic>();
		var current = new List<OrderBasic>();
		var future = new List<OrderBasic>();

		foreach(var order in all)
		{
			DateTime start = new DateTime(order.Date, order.Range.Begin);
			DateTime end = new DateTime(order.Date, order.Range.End);

			if (start > DateTime.Now)
			{
				future.Add(order);
			}
			else if(end < DateTime.Now)
			{
				past.Add(order);
			}
			else
			{
				current.Add(order);
			}
		}


		res["Past"] = past;
		res["Current"] = current;
		res["Future"] = future;
		return Task.FromResult<IDictionary<string, IEnumerable<OrderBasic>>>(res);
	}

	public async Task<OrderBasic> GetByIdAsync(long id)
	{
		return _orders[(int)id];
	}

	public async Task<OrderBasic> CreateOrderAsync(AnnouncementInfo announcement, DateOnly Date, TimeRange range)
	{
		var order = new OrderBasic
		{
			Id = _orders.Count,
			Date = Date,
			Range = range,
			User = _userService.GetCurrentUser(),
			Announcement = announcement,
		};

		_orders.Add(order);

		return order;
	}

	public async Task CancelOrder(long id)
	{
		var order = await GetByIdAsync(id);
		if(order.Status == OrderStatus.Upcoming)
			_orders.Remove(order);
	}
}
