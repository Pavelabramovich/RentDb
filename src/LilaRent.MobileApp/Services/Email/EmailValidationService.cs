using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.Services;


public class EmailValidationService : IEmailValidationService
{
    private readonly IFakeUserService _userService;


    public EmailValidationService(IFakeUserService userService)
    {
        _userService = userService; 
    }


    public Task<bool> IsExistAsync(string email)
    {
        return Task.FromResult(email.Length % 2 == 0);
    }

    public Task<bool> IsRegisteredAsync(string email)
    {
        return Task.FromResult(_userService.GetUsers().Any(u => u.Login == email));
    }
}
