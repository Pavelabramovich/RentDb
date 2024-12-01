using MediatR;


namespace LilaRent.Application.UseCases.Commands;


public record DeleteAnnouncementCommand(Guid Id) : IRequest;
