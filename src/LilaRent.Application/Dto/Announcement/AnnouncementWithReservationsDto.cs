namespace LilaRent.Application.Dto;


public record AnnouncementWithReservationsDto
{
    public required Guid Id { get; init; }
    public required string Image { get; init; }

    public required IEnumerable<ReservationSummaryDto> Reservations { get; init; }
}
