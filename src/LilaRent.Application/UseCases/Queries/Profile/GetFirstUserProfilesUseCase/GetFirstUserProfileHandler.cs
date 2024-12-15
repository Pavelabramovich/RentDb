using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Fields;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public class GetFirstUserProfileHandler : IRequestHandler<GetFirstUserProfileQuery, ProfileSummaryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public GetFirstUserProfileHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task<ProfileSummaryDto> Handle(GetFirstUserProfileQuery request, CancellationToken cancellationToken)
    {
        var login = request.login;

        var user = await _unitOfWork.UserRepository.FindByLoginAsync(login) ?? throw null;
        var userId = user.Id;

        var profiles = await _unitOfWork.ProfileRepository.GetAllAsync();

        var profile = profiles.Where(p => p.UserId == userId).OrderBy(p => p.Name).FirstOrDefault();

        if (profile is null)
            throw null;

        var imageUri = await _fileService.GetUri(profile.ImagePath);
        var legalEntityType = profile switch
        {
            LegalPersonProfile => LegalEntityType.LegalPerson,
            IndividualProfile => LegalEntityType.Individual,
            _ => throw null
        };

        return new ProfileSummaryDto()
        {
            Id = profile.Id,
            LegalEntityType = legalEntityType,
            Name = profile.Name,
            ImageUri = imageUri,
        };
    }
}
