using AutoMapper;
using LilaRent.Application.Exceptions;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Commands;


internal class UpdateAnnouncementHandler : IRequestHandler<UpdateAnnouncementCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public UpdateAnnouncementHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var duplicateName = await _unitOfWork.AnnouncementRepository
            .GetWhereAsync(a => a.RentObjectName == dto.RentObjectName && a.Id != dto.Id, cancellationToken);

        if (duplicateName.Any())
            throw new DuplicatedIdentifierException(dto.RentObjectName, $"Announcement with name = {dto.RentObjectName} already exists.");


        var announcement = await _unitOfWork.AnnouncementRepository.GetByIdWithImagesAsync(dto.Id, cancellationToken)
            ?? throw new EntityNotFoundException(dto.Id, $"Anoouncement with id = {dto.Id} not found.");

        var currentImages = announcement.Images;

        var newImages = dto.NewImages;                                        // add to storage
        var updatedImages = dto.UpdatedImages;                                // update indexes
        var deletedImages = dto.DeletedImages.Select(d => d.Split('/')[^1]);  // delete from storage

        var newImagesIdentifiers = await Task.WhenAll(newImages.Select(async image =>
        {
            var path = await _fileService.SaveFileAsync(image.Stream, image.Format.Extension);
            return new AnnouncementImage() { ImagePath = path, Index = image.Index, AnnouncementId = announcement.Id };
        }));

        foreach (var deletedImage in deletedImages)
        {
            await _fileService.DeleteFileAsync(deletedImage);
        }

        var finalImages = currentImages
            .Where(c => !deletedImages.Contains(c.ImagePath) && !updatedImages.Any(u => u.ImageIdentifier == c.ImagePath))
            .Concat(updatedImages.Select(u => new AnnouncementImage() { AnnouncementId = announcement.Id, ImagePath = u.ImageIdentifier, Index = u.NewIndex }))
            .Concat(newImagesIdentifiers)
            .OrderBy(i => i.Index)
            .ToList();

        var newOfferAgreementPath = dto.NewOfferAgreement is null
            ? null
            : await _fileService.SaveFileAsync(dto.NewOfferAgreement.Stream, dto.NewOfferAgreement.Format.Extension);

        var updatingAnnouncement = new Announcement()
        {
            Id = announcement.Id,
            ProfileId = announcement.ProfileId,
            RentObjectName = dto.RentObjectName,
            Address = dto.Address,
            Description = dto.Description,
            OpenTime = dto.OpenTime,
            CloseTime = dto.CloseTime,
            BreakBetweenReservations = dto.BreakBetweenReservations,
            Price = dto.Price,
            CanClientsChangeRecords = dto.CanClientsChangeRecords,
            CanClientsDisableRecords = dto.CanClientsDisableRecords,
            UseDiscount = dto.UseDiscount,
            IsPromoted = dto.IsPromoted,
            MinReservationTime = dto.MinReservationTime,
            MaxReservationTime = dto.MaxReservationTime,

            Images = finalImages,

            OfferAgreementPath = newOfferAgreementPath ?? announcement.OfferAgreementPath,
        };

        await _unitOfWork.AnnouncementRepository.UpdateWithImagesAsync(announcement.Id, updatingAnnouncement, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
