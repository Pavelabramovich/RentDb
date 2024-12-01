using LilaRent.Domain.Fields;


namespace LilaRent.Application.Dto;


public class FileCreatingDto
{
    public required FileFormat Format { get; init; }
    public required Stream Stream { get; init; }
}
