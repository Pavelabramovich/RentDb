using LilaRent.MobileApp.Entities;
using System.ComponentModel.DataAnnotations;


namespace LilaRent.MobileApp.Services;


public class UserListService : IFakeUserService
{
    private static User[] _users =
    [
        new() { Id = 0, Login = "Andrey@gmail.com", Password = "Pass1pass!", Profile = AppServiceProvider.Instance.GetRequiredService<IProfileService>().GetProfileById(0) },
        new() { Id = 1, Login = "Login@gmail.com", Password = "Password1!", Profile = AppServiceProvider.Instance.GetRequiredService<IProfileService>().GetProfileById(1) },
        new() { Id = 2, Login = "DON_HAMELEON@gmail.com", Password = "Hameleon1!", Profile = AppServiceProvider.Instance.GetRequiredService<IProfileService>().GetProfileById(2) }
    ];

    public User GetUserById(int id)
    {
        return _users.First(x => x.Id == id);
    }

    public IEnumerable<User> GetUsers()
    {
        return _users;
    }

    public ValidationResult ValidateLoginAndPassword(string login, string password)
    {
        var userByLogin = _users.FirstOrDefault(x => x.Login == login);

        if (userByLogin is null)
            return new ValidationResult("This login is not registered in the system", ["Login"]);

        if (userByLogin.Password != password)
            return new ValidationResult("Login or password is invalid", ["Password"]);

        return ValidationResult.Success!;
    }

    public Task<bool> ValidateVerificationCodeAsync(string login, string code)
    {
        int num = int.Parse(code);

        return Task.FromResult(num % 2 == 0);
    }

	public User GetCurrentUser()
	{
		return this.GetUserById(0);
	}

    public bool IsLoginExists(string login)
    {
        return _users.Any(x => x.Login == login);
    }
}
