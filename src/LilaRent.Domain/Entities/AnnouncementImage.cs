namespace LilaRent.Domain.Entities;


public record AnnouncementImage
{
    public virtual required string ImagePath { get; init; }

    public virtual required Guid AnnouncementId { get; init; }
    public virtual Announcement? Announcement { get; init; }

    public virtual required int Index { get; init; }
}
