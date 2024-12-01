using Microsoft.AspNetCore.Http;


namespace LilaRent.Requests.RequestModels;


public record ImageCreatingRequestModel
{
    public IFormFile Image { get; init; }
    public int Index { get; init; }
}
