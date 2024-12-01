using LilaRent.Application.Dto;
using LilaRent.Application.UseCases.Commands;
using LilaRent.Application.UseCases.Queries;
using LilaRent.Domain.Interfaces;
using LilaRent.Requests.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;


namespace LilaRent.WebApi.Controllers;


[ApiController]
[Route("announcements")]
public class AnnouncementsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IFileService fileService;


    public AnnouncementsController(IMediator mediator, IFileService fileService)
    {
        _mediator = mediator;
        this.fileService = fileService;
    }


    [HttpGet]
    public async Task<ActionResult<AnnouncementUpdatingDetailsDto>> GetAll()
    {
        var dto = await _mediator.Send(new GetAnnouncementsQuery());
        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AnnouncementUpdatingDetailsDto>> GetAnnouncementDetails([FromRoute] Guid id)
    {
        var dto = await _mediator.Send(new GetAnnouncementByIdQuery(id));
        return Ok(dto);
    }

    [HttpGet("2/{id:guid}")]
    public async Task<ActionResult<AnnouncementDetailsDto>> GetAnnouncementDetails2([FromRoute] Guid id)
    {
        var dto = await _mediator.Send(new GetAnnouncementById2Query(id));
        return Ok(dto);
    }


    [HttpGet("profile/{profileId:guid}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<AnnouncementSummaryDto>>> GetAnnouncementsByProfileId([FromRoute] Guid profileId)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var announcements = await _mediator.Send(new GetAnnouncementByProfileIdQuery(userId, profileId));
        return Ok(announcements);
    }

    [HttpPost]
    public async Task<IActionResult> PostAnnouncement(
        [FromForm] AnnouncementCreatingRequestModel request)
    {
        var images = request.Images.Select(i =>
        {
            var stream = i.OpenReadStream();
            var extension = Path.GetExtension(i.FileName).ToLower();

            return new FileCreatingDto()
            { 
                Format = extension,
                Stream = stream,
            };
        });

        var offerAgreement = new FileCreatingDto()
        {
            Format = Path.GetExtension(request.OfferAgreement.FileName).ToLower(),
            Stream = request.OfferAgreement.OpenReadStream(),
        };

        var announcementDto = new AnnouncementCreatingDto()
        {
            ProfileId = request.ProfileId,
            RentObjectName = request.RentObjectName,
            Address = request.Address,
            Description = request.Description,
            Price = request.Price,
            IsPromoted = request.IsPromoted,
            OpenTime = request.OpenTime,
            CloseTime = request.CloseTime,
            BreakBetweenReservations = request.BreakBetweenReservations,
            MinReservationTime = request.MinReservationTime,
            MaxReservationTime = request.MaxReservationTime,
            CanClientsChangeRecords = request.CanClientsChangeRecords,
            CanClientsDisableRecords = request.CanClientsDisableRecords,
            UseDiscount = request.UseDiscount,
            Images = images,
            OfferAgreement = offerAgreement,
        };

        await _mediator.Send(new CreateAnnouncementCommand(announcementDto));
        return NoContent();
    }


    [HttpPut]
    public async Task<IActionResult> PutAnnouncement(
        [FromForm] AnnouncementUpdatingRequestModel request)
    {
        var newImages = request.NewImages is null ? [] : request.NewImages.Select(i =>
        {
            var stream = i.Image.OpenReadStream();
            var extension = Path.GetExtension(i.Image.FileName).ToLower();

            return new ImageCreatingDto()
            {
                Format = extension,
                Stream = stream,
                Index = i.Index,
            };
        });

        var offerAgreement = request.NewOfferAgreement is null ? null : new FileCreatingDto()
        {
            Format = Path.GetExtension(request.NewOfferAgreement.FileName).ToLower(),
            Stream = request.NewOfferAgreement.OpenReadStream(),
        };

        var announcementDto = new AnnouncementUpdatingDto()
        {
            Id = request.Id,
            RentObjectName = request.RentObjectName,
            Address = request.Address,
            Description = request.Description,
            Price = request.Price,
            IsPromoted = request.IsPromoted,
            OpenTime = request.OpenTime,
            CloseTime = request.CloseTime,
            BreakBetweenReservations = request.BreakBetweenReservations,
            MinReservationTime = request.MinReservationTime ?? TimeSpan.Zero,
            MaxReservationTime = request.MaxReservationTime ?? TimeSpan.Zero,
            CanClientsChangeRecords = request.CanClientsChangeRecords,
            CanClientsDisableRecords = request.CanClientsDisableRecords,
            UseDiscount = request.UseDiscount,

            NewImages = newImages,
            UpdatedImages = request.UpdatedImages ?? [],
            DeletedImages = request.DeletedImages ?? [],

            NewOfferAgreement = offerAgreement,
        };

        await _mediator.Send(new UpdateAnnouncementCommand(announcementDto));
        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAnnouncement([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteAnnouncementCommand(id));
        return NoContent();
    }
}
