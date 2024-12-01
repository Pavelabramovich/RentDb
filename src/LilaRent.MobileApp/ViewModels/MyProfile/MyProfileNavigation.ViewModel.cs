using LilaRent.Domain.Fields;
using LilaRent.MobileApp.Core;
using System.ComponentModel;


namespace LilaRent.MobileApp.ViewModels;


public class MyProfileNavigationViewModel : NavigationViewModel
{
    private IProfileManager _profileManager;


    public MyProfileNavigationViewModel(IServiceProvider serviceProvider, IProfileManager profileManager)
        : base(serviceProvider)
    { 
        _profileManager = profileManager;

        _profileManager.PropertyChanged += ProfileManager_PropertyChanged;
        UpdateStack(profileManager.CurrentProfile.LegalEntityType);
    }

    private void ProfileManager_PropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != nameof(IProfileManager.CurrentProfile))
            return;

        UpdateStack(_profileManager.CurrentProfile.LegalEntityType);
    }

    private void UpdateStack(LegalEntityType legalEntityType)
    {
        if (!Enum.IsDefined(legalEntityType))
            throw new ArgumentException("Not defined LegaEntityType object.", nameof(legalEntityType));

        Type newRootType = legalEntityType == LegalEntityType.Individual
            ? typeof(MyIndividualProfileViewModel)
            : typeof(MyLegalPersonProfileViewModel);

        NavigationStack.PopToNewRoot(newRootType);
    }
}
