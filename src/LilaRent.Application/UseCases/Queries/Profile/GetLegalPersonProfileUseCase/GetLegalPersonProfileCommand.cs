using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetLegalPersonProfileCommand(Guid ProfileId) : IRequest<LegalPersonProfileDto>;
