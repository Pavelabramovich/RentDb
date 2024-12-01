using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Commands;


public record CreateAnnouncementCommand(AnnouncementCreatingDto Dto) : IRequest;
