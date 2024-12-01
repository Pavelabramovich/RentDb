using LilaRent.Domain.Fields;
using Microsoft.AspNetCore.Http;


namespace LilaRent.Requests.RequestModels;


public record UserWithProfileCreatingRequestModel
{
    public required string Login { get; init; }
    public required string Password { get; init; }

    public required LegalEntityType LegalEntityType { get; init; }

    public required string Name { get; init; }
    public required string Phone { get; init; }
    public required string Email { get; init; }
    public required string Description { get; init; }
    public required IFormFile Image { get; init; }
}
