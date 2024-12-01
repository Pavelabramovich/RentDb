
namespace LilaRent.MobileApp.Entities;


public class Category
{
	public long Id { get; set; }

	public string Name { get; set; }

	public Category(long Id, string Name)
	{
		this.Id = Id;
		this.Name = Name;
	}

	public static readonly Category RealEstate = new Category(Id: 0, Name: "Real estate");

	public static readonly Category Coworkings = new Category(Id: 1, Name: "Coworkings");

	public static readonly Category Equipment = new Category(Id: 2, Name: "Equipment");

	public static readonly Category Other = new Category(Id: 3, Name: "Other");
}
