using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


internal class GetAnnouncementsHandler : IRequestHandler<GetAnnouncementsQuery, IEnumerable<AnnouncementSummaryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public GetAnnouncementsHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task<IEnumerable<AnnouncementSummaryDto>> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
    {
        var announcements = await _unitOfWork.AnnouncementRepository.GetAllWithImagesAsync(cancellationToken);

        var res = await Task.WhenAll(announcements.Select(async a => new AnnouncementSummaryDto()
        { 
            Id = a.Id,
            RentObjectName = a.RentObjectName,
            Description = a.Description,
            Address = a.Address,
            ProfileName = "NULL",
            ProfileId = a.ProfileId,
            Price = a.Price,
            IsPromoted = a.IsPromoted,
            Images = await Task.WhenAll(a.Images.OrderBy(i => i.Index).Select(async i => await _fileService.GetUri(i.ImagePath)))
        }));

        return res;
    }
}
