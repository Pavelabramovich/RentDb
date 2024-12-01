using LilaRent.Application.Dto;
using LilaRent.MobileApp.ViewModels;
using Microsoft.Maui.Storage;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace LilaRent.MobileApp.Views;


public partial class UpdateAnnouncementFilesView : ContentPage
{
	public UpdateAnnouncementFilesView()
	{
		InitializeComponent();
	}


    public new UpdateAnnouncementFilesViewModel BindingContext => (UpdateAnnouncementFilesViewModel)base.BindingContext;


    private async void ContentButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();

            if (result is not null)
            {
                BindingContext.AddImagePathCommand.Execute(result.FullPath);
            }
        }
        catch (Exception ex)
        { }
    }


    private async void ContentButton2_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();

            if (result is not null)
            {
                BindingContext.NewOfferAgreementPath = result.FullPath;
            }
        }
        catch (Exception ex)
        { }
    }
}
