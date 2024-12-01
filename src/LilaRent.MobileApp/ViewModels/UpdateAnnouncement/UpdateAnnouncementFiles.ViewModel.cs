using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Dto;
using LilaRent.Application.Validation;
using LilaRent.Domain.Fields;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class UpdateAnnouncementFilesViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    private AnnouncementUpdatingDtoBuilder? _builder;

    public AnnouncementUpdatingDtoBuilder? Builder
    {
        get => _builder;
        set
        {
            _builder = value;

            Images = new(Builder.Images);
        }
    }

    [ObservableProperty]
    private ObservableCollection<ImageSource> _images;

    [ObservableProperty]
    private string? _newOfferAgreementPath;


    [ObservableProperty]
    private string? _imageError;

    [ObservableProperty]
    private string? _newOfferAgreementError;


    public UpdateAnnouncementFilesViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        Images = [];
        NewOfferAgreementPath = null;

        Images.CollectionChanged += Images_CollectionChanged;

        ImageError = null;
        NewOfferAgreementError = null;
    }

    [RelayCommand]
    private void AddImagePath(string imagePath)
    {
        if (Images.Count == 8)
            return;

        if (ValidateImagePath(imagePath) is string error)
        {
            ImageError = error;
            return;
        }

        ImageError = null;

        Images.Add(ImageSource.FromFile(imagePath));
    }

    [RelayCommand]
    private void RemovePhoto(ImageSource image)
    {
        Images.Remove(image);
    }

    [RelayCommand]
    private void Next()
    {
        if (!ValidateFields())
            return;

        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        Builder.Images = Images;
        Builder.NewOfferAgreementPath = NewOfferAgreementPath; 

        _navigationService.CurrentTabs.Navigation.Push<UpdateAnnouncementScheduleViewModel>(new { Builder });
    }

    private bool ValidateFields()
    {
        var isValid = true;

        if (Images.Count == 0)
        {
            ImageError = "Add at least one image";
            isValid = false;
        }

        return isValid;
    }

    partial void OnNewOfferAgreementPathChanged(string? value)
    {
        var offerAgreementPath = value;

        if (offerAgreementPath is null)
            return;

        if (ValidateOfferAgreementPath(offerAgreementPath) is string error)
        {
            NewOfferAgreementError = error;
            NewOfferAgreementPath = null;
            return;
        }
       
        NewOfferAgreementError = null;
    }

    private void Images_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        ImageError = null;
    }


    private static string? ValidateImagePath(string path)
    {
        var extension = new FileFormat(Path.GetExtension(path));
        var size = new FileInfo(path).Length;

        if (!ImageConstraints.AllowedFormats.Contains(extension))
        {
            return ImageConstraints.IncorrectFormatError.Replace("{PropertyName}", "image");
        }
        else if (size > ImageConstraints.MaxFileSizeInBytes)
        {
            return ImageConstraints.MaxFileSizeError.Replace("{PropertyName}", "image");
        }

        return null;
    }

    private static string? ValidateOfferAgreementPath(string path)
    {
        var extension = new FileFormat(Path.GetExtension(path));
        var size = new FileInfo(path).Length;

        if (!OfferAgreementConstraints.AllowedFormats.Contains(extension))
        {
            return OfferAgreementConstraints.IncorrectFormatError.Replace("{PropertyName}", "offer agreement");
        }
        else if (size > OfferAgreementConstraints.MaxFileSizeInBytes)
        {
            return OfferAgreementConstraints.MaxFileSizeError.Replace("{PropertyName}", "offer agreement");
        }

        return null;
    }
}
