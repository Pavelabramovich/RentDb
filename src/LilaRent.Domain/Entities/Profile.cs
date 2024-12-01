namespace LilaRent.Domain.Entities;


public record Profile
{
    public virtual Guid Id { get; } = Guid.NewGuid();

    public virtual required Guid UserId { get; init; }
    public virtual User? User { get; init; }

    public virtual required string Name { get; init; }
    public virtual required string Phone { get; init; }
    public virtual required string Email { get; init; }

    public virtual required string Description { get; init; }
    public virtual required string ImagePath { get; init; }
}
