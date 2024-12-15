namespace LilaRent.Application.Dto;


public class ReservationCreatingDto
{
    public required Guid ClientId { get; init; }

    public required Guid AnnouncementId { get; init; }

    public required DateTime Begin { get; init; }
    public required DateTime End { get; init; }

    public required DateTime CreatedAt { get; init; }
}
