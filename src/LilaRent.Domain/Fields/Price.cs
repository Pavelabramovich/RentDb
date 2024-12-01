namespace LilaRent.Domain.Fields;


public record Price
{
    public required decimal Value { get; init; }
    public required TimeUnit TimeUnit { get; init; }
}
