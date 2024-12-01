namespace LilaRent.Requests.RequestModels;


public record LoginRequestModel
{
    public string Login { get; init; }
    public string Password { get; init; }
}
