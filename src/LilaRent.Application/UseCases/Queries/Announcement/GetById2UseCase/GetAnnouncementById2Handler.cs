using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Application.Exceptions;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


internal class GetAnnouncementById2Handler : IRequestHandler<GetAnnouncementById2Query, AnnouncementDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public GetAnnouncementById2Handler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<AnnouncementDetailsDto> Handle(GetAnnouncementById2Query request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var announcement = await _unitOfWork.AnnouncementRepository.GetByIdWithImagesAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(id, $"Announcement with id = {id} does not exists");

        var imageDtos = await Task.WhenAll(announcement.Images.Select(async image =>
        {
            return new FileDto()
            {
                Identifier = image.ImagePath,
                Uri = await _fileService.GetUri(image.ImagePath)
            };
        }));

        var offerAgreementDto = new FileDto()
        {
            Identifier = announcement.OfferAgreementPath,
            Uri = await _fileService.GetUri(announcement.OfferAgreementPath)
        };

        var announcementDto = new AnnouncementDetailsDto()
        {
            Id = announcement.Id,
            RentObjectName = announcement.RentObjectName,
           // Address = announcement.Address,
            Description = announcement.Description,
            Price = announcement.Price,
            IsPromoted = announcement.IsPromoted,
            ProfileId = announcement.ProfileId,
            ProfileName = announcement.Profile.Name,

            //OpenTime = announcement.OpenTime,
            //CloseTime = announcement.CloseTime,
            //BreakBetweenReservations = announcement.BreakBetweenReservations,
            //MinReservationTime = announcement.MinReservationTime ?? TimeSpan.Zero,
            //MaxReservationTime = announcement.MaxReservationTime ?? TimeSpan.Zero,
            //CanClientsChangeRecords = announcement.CanClientsChangeRecords,
            //CanClientsDisableRecords = announcement.CanClientsDisableRecords,
            //UseDiscount = announcement.UseDiscount,
            //MinTimeForDiscount = announcement.MinTimeForDiscount,
            //MaxTimeForDiscount = announcement.MaxTimeForDiscount,
            //DiscountPercentage = announcement.DiscountPercentage,

            Images = imageDtos,
            OfferAgreement = offerAgreementDto,

            SimularAnnouncements = []
        };

        return announcementDto;
    }
}
