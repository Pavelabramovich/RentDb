using LilaRent.Domain.Fields;


namespace LilaRent.Application.Validation;


public static class OfferAgreementConstraints
{
    public static readonly HashSet<FileFormat> AllowedFormats = [FileFormat.Pdf, FileFormat.Doc];
    public const string IncorrectFormatError = "{PropertyName} allows only .pdf and .doc formats";

    public const long MaxFileSizeInBytes = 3 * 1024 * 1024;
    public const string MaxFileSizeError = "maximum {PropertyName} size is 3 MB";
}
