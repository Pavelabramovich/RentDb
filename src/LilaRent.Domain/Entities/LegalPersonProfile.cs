namespace LilaRent.Domain.Entities;


public record LegalPersonProfile : Profile
{
    public virtual string? Address { get; set; } = null;
    public virtual int? Floor { get; set; } = null;
    public virtual string? Station { get; set; } = null;
    public virtual decimal? Area { get; set; } = null;

    public virtual ICollection<Announcement> Announcements { get; init; } = new List<Announcement>();
    public virtual ICollection<ClientColor> ClientColors { get; init; } = new List<ClientColor>();
}
