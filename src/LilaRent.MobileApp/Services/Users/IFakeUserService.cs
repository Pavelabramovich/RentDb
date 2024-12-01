using LilaRent.MobileApp.Entities;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;


namespace LilaRent.MobileApp.Services;


public interface IFakeUserService
{
    IEnumerable<User> GetUsers();

    User GetUserById(int id);

    ValidationResult ValidateLoginAndPassword(string login, string password);

    bool IsLoginExists(string login);

    Task<bool> ValidateVerificationCodeAsync(string login, string code);

	User GetCurrentUser();
}
