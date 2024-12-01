using LilaRent.Domain.Fields;


namespace LilaRent.Application.Validation;


public static class ImageConstraints
{
    public static readonly HashSet<FileFormat> AllowedFormats = [FileFormat.Png, FileFormat.Jpeg, FileFormat.Svg];
    public const string IncorrectFormatError = "{PropertyName} allows only .png, .jpg, .jpeg and .svg formats";

    public const long MaxFileSizeInBytes = 5 * 1024 * 1024;  
    public const string MaxFileSizeError = "maximum {PropertyName} size is 5 MB";
}
