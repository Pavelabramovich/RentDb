namespace LilaRent.Domain.Entities;


public record RefreshToken
{
    public virtual required Guid UserId { get; init; }

    public virtual required string Value { get; init; }
    public virtual required DateTime Expires { get; init; }
}
