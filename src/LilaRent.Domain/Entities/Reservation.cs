namespace LilaRent.Domain.Entities;


public record Reservation
{
    public virtual Guid Id { get; } = Guid.NewGuid();

    public virtual required Guid ProfileId { get; init; }
    public virtual Profile? Profile { get; init; }

    public virtual required Guid AnnouncementId { get; init; }
    public virtual Announcement? Announcement { get; init; }

    public virtual required DateTime Begin { get; init; }
    public virtual required DateTime End { get; init; }

    public virtual required DateTime CreatedAt { get; init; }
    public virtual DateTime? ChangedAt { get; init; }
}
