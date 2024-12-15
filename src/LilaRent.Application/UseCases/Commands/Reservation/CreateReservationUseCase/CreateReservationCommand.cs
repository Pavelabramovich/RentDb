using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Commands;


public record CreateReservationCommand(ReservationCreatingDto Dto) : IRequest;
