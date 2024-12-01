namespace LilaRent.Domain.Entities;


public record Review
{
    public virtual Guid Id { get; } = Guid.NewGuid();

    public virtual required Guid AuthorId { get; init; }
    public virtual Profile? Author { get; init; }

    public virtual required Guid AnnouncementId { get; init; }
    public virtual Announcement? Announcement { get; init; }

    public virtual required string Value { get; set; }
    public virtual required float? Rating { get; set; }
    public virtual required DateTime CreatedAt { get; init; }
}
