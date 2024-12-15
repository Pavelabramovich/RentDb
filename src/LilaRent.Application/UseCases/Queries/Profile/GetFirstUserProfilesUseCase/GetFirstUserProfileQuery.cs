using LilaRent.Application.Dto;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public record GetFirstUserProfileQuery(string login) : IRequest<ProfileSummaryDto>;
