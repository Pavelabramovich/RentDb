using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Domain.Interfaces;
using MediatR;

namespace LilaRent.Application.UseCases.Queries;


public class GetPreviosReservedAnnouncementsHandler : IRequestHandler<GetPreviosReservedAnnouncementsQuery, IEnumerable<AnnouncementSummaryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public GetPreviosReservedAnnouncementsHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task<IEnumerable<AnnouncementSummaryDto>> Handle(GetPreviosReservedAnnouncementsQuery request, CancellationToken cancellationToken)
    {
        var id = request.IndividualId;

        var anoouncements = await _unitOfWork.IndividualProfileRepository.GetPrevioslyReservedAnnouncements(id);

        var res = await Task.WhenAll(anoouncements.Select(async a => new AnnouncementSummaryDto()
        { 
            Id = a.Id,
            RentObjectName = a.RentObjectName,
            Description = a.Description,
            ProfileName = "NULL",
            ProfileId = a.ProfileId,
            Price = a.Price,
            Address = a.Address,
            IsPromoted = a.IsPromoted,
            Images = await Task.WhenAll(a.Images.Select(async i => await _fileService.GetUri(i.ImagePath)))
        }));

        return res;
    }
}
