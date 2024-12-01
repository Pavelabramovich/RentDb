using MediatR;


namespace LilaRent.Application.UseCases.Commands;


public record LogoutCommand(Guid UserId) : IRequest;
