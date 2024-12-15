using FluentAssertions;
using LilaRent.Application.Dto;
using LilaRent.Application.Exceptions;
using LilaRent.Application.UseCases.Commands;
using LilaRent.Application.UseCases.Queries;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Fields;


namespace LilaRent.Application.Tests.UseCasesTests.CommandsTests;


public class UpdateAnnouncementUseCaseTests : BaseUseCaseTest
{
    [Fact]
    public async Task UpdateAnnouncementUseCase_ShouldUpdate()
    {
        // Arrange
        var announcementDto = new AnnouncementUpdatingDto()
        {
            Id = _announcements.First().Id, 
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
        };


        // Act
        await _mediator.Send(new UpdateAnnouncementCommand(announcementDto));


        // Assert
        _announcements.Last().Should().BeEquivalentTo(new Announcement()
        {
            ProfileId = _announcements.First().ProfileId,
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


    [Fact]
    public async Task ThrowEntityNotFound_IfNotFound()
    {
        // Arrange
        var unexistedId = Guid.NewGuid();

        var announcementDto = new AnnouncementUpdatingDto()
        {
            Id = unexistedId,
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
        };


        // Act
        var act = async () => await _mediator.Send(new UpdateAnnouncementCommand(announcementDto));


        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException>();
    }
}
