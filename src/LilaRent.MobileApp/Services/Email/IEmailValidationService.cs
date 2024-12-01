using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.Services;


public interface IEmailValidationService
{
    Task<bool> IsExistAsync(string email);

    Task<bool> IsRegisteredAsync(string email);
}
