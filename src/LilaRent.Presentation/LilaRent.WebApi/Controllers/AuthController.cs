using LilaRent.Application.Dto;
using LilaRent.Application.UseCases.Commands;
using LilaRent.Requests.RequestModels;
using LileRent.Application.UseCases.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace LilaRent.WebApi.Controllers;


[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private IMediator _mediator;


    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<TokensDto> Login([FromBody] LoginRequestModel request)
    {
        var credentials = new CredentialsDto() { Login = request.Login, Password = request.Password };

        return await _mediator.Send(new LoginCommand(credentials));
    }

    [HttpPost("refresh")]
    public async Task<TokensDto> RefreshToken([FromBody] RefreshRequestModel request)
    {
        var tokens = new TokensDto() { AccessToken =  request.AccessToken, RefreshToken = request.RefreshToken };

        return await _mediator.Send(new RefreshCommand(tokens));
    }

    [HttpPost("logout/{userId:guid}")]
    public async Task Logout([FromRoute] Guid userId)
    {
        await _mediator.Send(new LogoutCommand(userId));
    }
}
