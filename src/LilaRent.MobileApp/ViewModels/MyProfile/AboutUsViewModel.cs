using CommunityToolkit.Mvvm.Input;


namespace LilaRent.MobileApp.ViewModels;


public partial class AboutUsViewModel : IViewModel
{
    [RelayCommand]
    public async Task ToSite()
    {
        await Launcher.OpenAsync("https://lila-creation.by/");
    }
}
