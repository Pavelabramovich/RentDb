using LilaRent.Application.Dto;
using LilaRent.Application.UseCases.Queries;
using LilaRent.Domain.Fields;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace LilaRent.WebApi.Controllers;


[ApiController]
[Route("profiles")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;


    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("of-{userLogin}")]
    public async Task<ActionResult<ProfileSummaryDto>> GetFirstProfile([FromRoute] string userLogin)
    {
        var profile = await _mediator.Send(new GetFirstUserProfileQuery(userLogin));
        return Ok(profile);
    }

    [HttpGet("owner/{id:guid}")]
    public async Task<ActionResult<LegalPersonProfileDto>> GetLegalPersonProfile([FromRoute] Guid id)
    {
        var profile = await _mediator.Send(new GetLegalPersonProfileQuery(id));
        return Ok(profile);
    }

    [HttpGet("previos/{id:guid}")]
    public async Task<ActionResult<IEnumerable<AnnouncementSummaryDto>>> GetPrevios([FromRoute] Guid id)
    {
        var announcements = await _mediator.Send(new GetPreviosReservedAnnouncementsQuery(id));
        return Ok(announcements);
    }

    [HttpGet("reservations/{id:guid}/{type}")]
    public async Task<ActionResult<IEnumerable<ReservationSummaryDto>>> GetReservations([FromRoute] Guid id, [FromRoute] string type)
    {
        var legalEntityType = type.StartsWith("l") 
            ? LegalEntityType.LegalPerson 
            : LegalEntityType.Individual;

        var reservations = await _mediator.Send(new GetProfileReservationQuery(id, legalEntityType));
        return Ok(reservations);
    }

    //[HttpPost]
    //public async Task<IActionResult> PostUser(
    //    [FromForm] UserWithProfileCreatingRequestModel request)
    //{
    //    var image = new FileCreatingDto()
    //    {
    //        Format = Path.GetExtension(request.Image.FileName).ToLower(),
    //        Stream = request.Image.OpenReadStream(),
    //    };

    //    var userDto = new UserWithProfileCreatingDto()
    //    {
    //        Login = request.Login,
    //        Password = request.Password,

    //        Profile = new ProfileCreatingDto()
    //        {
    //            LegalEntityType = request.LegalEntityType,
    //            Name = request.Name,
    //            Phone = request.Phone,
    //            Email = request.Email,
    //            Description = request.Description,
    //            Image = image,
    //        }
    //    };

    //    await _mediator.Send(new CreateUserWithProfileCommand(userDto));
    //    return NoContent();
    //}
}
