using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetFirstUserProfileCommand(string login) : IRequest<ProfileSummaryDto>;
