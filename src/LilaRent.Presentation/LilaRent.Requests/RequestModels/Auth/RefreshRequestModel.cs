namespace LilaRent.Requests.RequestModels;


public record RefreshRequestModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
