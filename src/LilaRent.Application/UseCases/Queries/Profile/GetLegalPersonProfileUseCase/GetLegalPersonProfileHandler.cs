using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public class GetLegalPersonProfileHandler : IRequestHandler<GetLegalPersonProfileCommand, LegalPersonProfileDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public GetLegalPersonProfileHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task<LegalPersonProfileDto> Handle(GetLegalPersonProfileCommand request, CancellationToken cancellationToken)
    {
        var id = request.ProfileId;

        var profile = (LegalPersonProfile)await _unitOfWork.ProfileRepository.FindByIdAsync(id);

        return new LegalPersonProfileDto()
        {
            Id = profile.Id,
            Name = profile.Name,
            Description = profile.Description,
            Image = await _fileService.GetUri(profile.ImagePath),
            Announcements = []
        };
    }
}
