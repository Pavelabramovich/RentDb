using LilaRent.Application.Dto;
using MediatR;


namespace LileRent.Application.UseCases.Commands;


public record LoginCommand(CredentialsDto CredentialsDto) : IRequest<TokensDto>;
