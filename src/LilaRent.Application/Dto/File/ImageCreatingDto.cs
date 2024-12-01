using LilaRent.Domain.Fields;


namespace LilaRent.Application.Dto;


public class ImageCreatingDto
{
    public required FileFormat Format { get; init; }
    public required Stream Stream { get; init; }
    public required int Index { get; init; }
}
