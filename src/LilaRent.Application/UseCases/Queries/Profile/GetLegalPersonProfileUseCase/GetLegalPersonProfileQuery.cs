using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetLegalPersonProfileQuery(Guid ProfileId) : IRequest<LegalPersonProfileDto>;
