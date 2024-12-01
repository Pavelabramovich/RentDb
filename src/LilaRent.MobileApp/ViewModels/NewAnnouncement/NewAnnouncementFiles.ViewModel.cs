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
public partial class NewAnnouncementFilesViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly OfferAgreementValidator _offerAgreementValidator;
    private readonly ImageValidator _imageValidator;

    public AnnouncementCreatingDtoBuilder? Builder { get; set; }

    public ObservableCollection<string> ImagePaths { get; }

    [ObservableProperty]
    private string? _offerAgreementPath;


    [ObservableProperty]
    private string? _imageError;

    [ObservableProperty]
    private string? _offerAgreementError;


  //  private Dictionary<string, FileCreatingDto> _pathDtos;


    public NewAnnouncementFilesViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

      //  _pathDtos = [];

        _imageValidator = new ImageValidator("image");
        _offerAgreementValidator = new OfferAgreementValidator("offer agreement");

        ImagePaths = [];
        OfferAgreementPath = null;

        ImagePaths.CollectionChanged += Images_CollectionChanged;

        ImageError = null;
        OfferAgreementError = null;
    }

    [RelayCommand]
    private void AddImagePath(string imagePath)
    {
        if (ImagePaths.Count == 8)
            return;

        if (ValidateImagePath(imagePath) is string error)
        {
            ImageError = error;
            return;
        }

        ImageError = null;

        ImagePaths.Add(imagePath);
    }

    [RelayCommand]
    private void RemovePhoto(string file)
    {
        ImagePaths.Remove(file);
    }

    [RelayCommand]
    private void Next()
    {
        if (!ValidateFields())
            return;

        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        Builder.ImagePaths = ImagePaths;
        Builder.OfferAgreementPath = OfferAgreementPath; 

        _navigationService.CurrentTabs.Navigation.Push<NewAnnouncementScheduleViewModel>(new { Builder });
    }

    private bool ValidateFields()
    {
        var isValid = true;

        if (!string.IsNullOrEmpty(OfferAgreementError))
            isValid = false;

        if (ImagePaths.Count == 0)
        {
            ImageError = "Add at least one image";
            isValid = false;
        }

        if (OfferAgreementPath is null)
        {
            OfferAgreementError = "offer agreement is required";
            isValid = false;
        }

        return isValid;
    }

    partial void OnOfferAgreementPathChanged(string? value)
    {
        var offerAgreementPath = value;

        if (offerAgreementPath is null)
            return;

        if (ValidateOfferAgreementPath(offerAgreementPath) is string error)
        {
            OfferAgreementError = error;
            OfferAgreementPath = null;
            return;
        }
       
        OfferAgreementError = null;
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
