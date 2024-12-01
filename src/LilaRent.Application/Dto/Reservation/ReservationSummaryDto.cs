namespace LilaRent.Application.Dto;


public record ReservationSummaryDto
{
    public required Guid Id { get; init; }

    public required string ProfileName { get; init; }

    public required DateTime Begin { get; init; }
    public required DateTime End { get; init; }
}
