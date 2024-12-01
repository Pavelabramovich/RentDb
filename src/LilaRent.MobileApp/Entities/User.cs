
namespace LilaRent.MobileApp.Entities;


public class User
{
    public int Id { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }

    public Profile Profile { get; set; }
}
