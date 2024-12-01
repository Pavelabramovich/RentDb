using LilaRent.Domain.Fields;


namespace LilaRent.Application.Dto;


public record ReservationDetailsDto
{
    public required Guid Id { get; init; }

    public required string RentObjectName { get; set; }

    public required IndividualProfileDto Profile { get; init; }

    public required Price Price { get; init; }

    public required DateTime Begin { get; init; }
    public required DateTime End { get; init; }
}
