using LilaRent.Domain.Fields;


namespace LilaRent.Domain.Entities;


public record Announcement
{
    public virtual Guid Id { get; init; } = Guid.NewGuid();

    public virtual required Guid ProfileId { get; init; }
    public virtual LegalPersonProfile? Profile { get; init; }

    public virtual required string RentObjectName { get; set; }
    public virtual required string Address { get; set; }
    public virtual required string Description { get; set; }

    public virtual ICollection<AnnouncementImage> Images { get; init; } = new List<AnnouncementImage>();
    public virtual required string OfferAgreementPath { get; init; }

    public virtual required TimeOnly OpenTime { get; set; }
    public virtual required TimeOnly CloseTime { get; set; }
    public virtual required TimeSpan BreakBetweenReservations { get; set; }

    public virtual required bool CanClientsChangeRecords { get; set; }
    public virtual required bool CanClientsDisableRecords { get; set; }
    
    public virtual TimeSpan? MinReservationTime { get; set; }
    public virtual TimeSpan? MaxReservationTime { get; set; }
    public virtual required Price Price { get; set; }
    public virtual required bool UseDiscount { get; set; }
    public virtual required bool IsPromoted { get; set; }

    public virtual TimeSpan? MinTimeForDiscount { get; set; }
    public virtual TimeSpan? MaxTimeForDiscount { get; set; }
    public virtual int? DiscountPercentage { get; set; }

    public virtual ICollection<Reservation> Reservations { get; init; } = new List<Reservation>();
}
