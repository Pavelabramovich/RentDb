
namespace LilaRent.MobileApp.Services;


public class ApplicationService : IApplicationService
{
    public App CurrentApplication
    {
        get => (App)(Microsoft.Maui.Controls.Application.Current 
            ?? throw new InvalidOperationException("Can't get current application"));
    }
}
