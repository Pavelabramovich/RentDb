namespace LilaRent.Domain.Entities;


public record IndividualProfile : Profile
{
    public virtual ICollection<Reservation> Reservatinos { get; init; } = new List<Reservation>();
}
