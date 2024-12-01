using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Commands;


public record RefreshCommand(TokensDto TokensDto) : IRequest<TokensDto>;
