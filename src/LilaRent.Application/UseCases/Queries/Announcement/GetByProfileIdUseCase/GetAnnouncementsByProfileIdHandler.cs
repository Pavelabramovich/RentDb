using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Application.Exceptions;
using LilaRent.Domain.Interfaces;
using MediatR;
using Profile = LilaRent.Domain.Entities.Profile;


namespace LilaRent.Application.UseCases.Queries;


internal class GetAnnouncementsByProfileIdHandler : IRequestHandler<GetAnnouncementByProfileIdQuery, IEnumerable<AnnouncementSummaryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public GetAnnouncementsByProfileIdHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task<IEnumerable<AnnouncementSummaryDto>> Handle(GetAnnouncementByProfileIdQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var profileId = request.ProfileId;
       
        if (await _unitOfWork.ProfileRepository.FindByIdAsync(profileId) is not Profile profile)
        {
            throw new EntityNotFoundException(profileId);
        }
        else if (profile.UserId != userId)
        {
            throw new InvalidOperationException("This user is not profile owner.");
        }

        var announcements = await _unitOfWork.AnnouncementRepository.GetByProfileIdAsync(profileId, cancellationToken);

        return await Task.WhenAll(announcements.Select(async a => new AnnouncementSummaryDto()
        {
            Id = a.Id,
            RentObjectName = a.RentObjectName,
            Description = a.Description,
            Address = a.Address,
            ProfileName = a.Profile.Name,
            ProfileId = profileId,
            Price = a.Price,
            IsPromoted = a.IsPromoted,
            Images = await Task.WhenAll(a.Images.OrderBy(i => i.Index).Select(async i => await _fileService.GetUri(i.ImagePath)))
        }));
    }
}
