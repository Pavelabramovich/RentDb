namespace LilaRent.Domain.Fields;


public readonly struct FileFormat
{
    public static readonly FileFormat Png = new(".png");
    public static readonly FileFormat Jpeg = new(".jpeg");
    public static readonly FileFormat Svg = new(".svg");
    public static readonly FileFormat Doc = new(".doc");
    public static readonly FileFormat Pdf = new(".pdf");


    public string Extension { get; }


    public FileFormat(string extension)
    {
        Extension = NormalizeExtension(extension);
    }


    public static bool operator == (FileFormat left, FileFormat right)
    {
        return left.Extension == right.Extension;
    }

    public static bool operator != (FileFormat left, FileFormat right)
    {
        return !(left == right);
    }

    public static implicit operator FileFormat(string extension)
    {
        return new FileFormat(extension);
    }


    public override bool Equals(object? obj)
    {
        if (obj is not FileFormat fileFormat)
            return false;

        return this == fileFormat;
    }

    public override int GetHashCode()
    {
        return Extension.GetHashCode();
    }

    public override string ToString()
    {
        return Extension;
    }


    private static string NormalizeExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension))
            throw new ArgumentException("Empty extension", nameof(extension));

        extension = extension.Trim().ToLower();

        if (extension[0] != '.')
            extension = $".{extension}";

        if (extension.Count(c => c == '.') > 1)
            throw new ArgumentException("Extension contains extra dots", nameof(extension));

        if (extension.Any(c => char.IsWhiteSpace(c)))
            throw new ArgumentException("Extension contains spaces", nameof(extension));

        if (extension == ".jpg")
            extension = ".jpeg";

        return extension;
    }
}
