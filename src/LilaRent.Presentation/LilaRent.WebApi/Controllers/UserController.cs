using LilaRent.Application.Dto;
using LilaRent.Application.UseCases.Commands;
using LilaRent.Requests.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace LilaRent.WebApi.Controllers;


[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;


    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> PostUser(
        [FromForm] UserWithProfileCreatingRequestModel request)
    {
        var image = new FileCreatingDto()
        {
            Format = Path.GetExtension(request.Image.FileName).ToLower(),
            Stream = request.Image.OpenReadStream(),
        };

        var userDto = new UserWithProfileCreatingDto()
        {
            Login = request.Login,
            Password = request.Password,

            Profile = new ProfileCreatingDto()
            {
                LegalEntityType = request.LegalEntityType,
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                Description = request.Description,
                Image = image,
            }
        };

        await _mediator.Send(new CreateUserWithProfileCommand(userDto));
        return NoContent();
    }
}
