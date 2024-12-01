using System.Drawing;


namespace LilaRent.Domain.Entities;


public record ClientColor
{
    public virtual required Guid OwnerId { get; init; }
    public virtual LegalPersonProfile? Owner { get; init; }

    public virtual required Guid ClientId { get; init; }
    public virtual IndividualProfile? Client { get; init; }

    public virtual required Color Color { get; set; }
}
