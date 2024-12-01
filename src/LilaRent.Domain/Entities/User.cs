namespace LilaRent.Domain.Entities;


public record User
{
    public virtual Guid Id { get; } = Guid.NewGuid();

    public virtual required string Login { get; set; }
    public virtual required string HashedPassword { get; set; }
    public virtual required string Salt { get; init; }

    public virtual ICollection<Profile> Profiles { get; init; } = new List<Profile>();
}
