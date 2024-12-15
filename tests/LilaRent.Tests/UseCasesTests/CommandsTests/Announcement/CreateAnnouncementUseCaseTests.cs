using FluentAssertions;
using LilaRent.Application.Dto;
using LilaRent.Application.UseCases.Commands;
using LilaRent.Domain.Fields;
using LilaRent.Domain.Entities;


namespace LilaRent.Application.Tests.UseCasesTests.CommandsTests;


public class CreateAnnouncementUseCaseTests : BaseUseCaseTest
{
    [Fact]
    public async Task CreateAnnouncementUseCase_ShouldCreate()
    {
        // Arrange
        var announcementDto = new AnnouncementCreatingDto()
        {
            ProfileId = _legalPersonProfiles.First().Id,
            RentObjectName = "rent",
            Address = "addressss",
            Description = "desc",
            Price = new() { TimeUnit = TimeUnit.Hours, Value = 1 },
            IsPromoted = false,
            OpenTime = TimeOnly.MinValue,
            CloseTime = TimeOnly.MaxValue,
            BreakBetweenReservations = TimeSpan.Zero,
            CanClientsChangeRecords = false,
            CanClientsDisableRecords = false,
            UseDiscount = false,
            Images = [],
            OfferAgreement = new FileCreatingDto() { Format = new FileFormat(".png"), Stream = new MemoryStream("rfshnhe"u8.ToArray()) },    
        };


        // Act
        await _mediator.Send(new CreateAnnouncementCommand(announcementDto));


        // Assert
        _announcements.Last().Should().BeEquivalentTo(new Announcement()
        {
            ProfileId = announcementDto.ProfileId,
            RentObjectName = announcementDto.RentObjectName,
            Address = announcementDto.Address,
            Description = announcementDto.Description,
            OfferAgreementPath = default,
            OpenTime = announcementDto.OpenTime,
            CloseTime = announcementDto.CloseTime,
            BreakBetweenReservations = announcementDto.BreakBetweenReservations,
            CanClientsChangeRecords = announcementDto.CanClientsChangeRecords,
            CanClientsDisableRecords = announcementDto.CanClientsDisableRecords,
            Price = announcementDto.Price,
            UseDiscount = announcementDto.UseDiscount,
            IsPromoted = announcementDto.IsPromoted
        },
        options => options
            .Excluding(a => a.Id)
            .Excluding(a => a.OfferAgreementPath)
            .Excluding(a => a.Images)
        );
    }
}
