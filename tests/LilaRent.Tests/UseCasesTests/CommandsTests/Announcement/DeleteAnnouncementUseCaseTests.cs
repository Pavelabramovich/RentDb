using FluentAssertions;
using LilaRent.Application.Exceptions;
using LilaRent.Application.UseCases.Commands;


namespace LilaRent.Application.Tests.UseCasesTests.CommandsTests;


public class DeleteAnnouncementUseCaseTests : BaseUseCaseTest
{
    [Fact]
    public async Task DeleteAnnoucementUseCase_DeleteCorrect()
    {
        // Arrange
        var id = _announcements.First().Id;


        // Act
        await _mediator.Send(new DeleteAnnouncementCommand(id));


        // Assert
        _announcements.Should().NotContain(a => a.Id == id);
    }


    [Fact]
    public async Task ThrowEntityNotFound_IfAnnouncementDoesNotExists()
    {
        // Arrange
        var unexistedId = Guid.NewGuid();


        // Act
        var act = async () => await _mediator.Send(new DeleteAnnouncementCommand(unexistedId));


        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException>();
    }
}
