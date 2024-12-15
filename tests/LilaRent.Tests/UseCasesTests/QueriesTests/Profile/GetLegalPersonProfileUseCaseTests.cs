using FluentAssertions;
using LilaRent.Application.Dto;
using LilaRent.Application.Exceptions;
using LilaRent.Application.UseCases.Queries;

namespace LilaRent.Application.Tests.UseCasesTests.QueriesTests;


public class GetLegalPersonProfileUseCaseTests : BaseUseCaseTest
{
    [Fact]
    public async Task GetLegalPersonProfile_Should_Work()
    {
        // Arrange
        var profileId = _legalPersonProfiles.First().Id;


        // Act
        var profile = await _mediator.Send(new GetLegalPersonProfileQuery(profileId));


        // Assert
        profile.Should().BeEquivalentTo(new LegalPersonProfileDto()
        {
            Id = profileId,
            Name = _legalPersonProfiles.First().Name,
            Description = _legalPersonProfiles.First().Description,
            Image = default,
            Announcements = []
        },
        options => options
            .Excluding(a => a.Image)
            .Excluding(a => a.Announcements)
        );
    }

    [Fact]
    public async Task ThrowNotFouncException_IfNotFound()
    {
        // Arrange
        var unexistedId = Guid.NewGuid();


        // Act
        var act = async () => await _mediator.Send(new GetLegalPersonProfileQuery(unexistedId));


        // Assert
        await act.Should().ThrowAsync<NullReferenceException>();
    }
}
