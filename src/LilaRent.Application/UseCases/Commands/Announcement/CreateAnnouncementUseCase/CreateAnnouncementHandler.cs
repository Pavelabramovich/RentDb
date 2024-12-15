using AutoMapper;
using LilaRent.Application.Exceptions;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Commands;


internal class CreateAnnouncementHandler : IRequestHandler<CreateAnnouncementCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public CreateAnnouncementHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var duplicateName = await _unitOfWork.AnnouncementRepository.GetAllAsync();
        duplicateName = duplicateName.Where(a => a.RentObjectName == dto.RentObjectName);

        if (duplicateName.Any())
            throw new DuplicatedIdentifierException(dto.RentObjectName, $"Announcement with name = {dto.RentObjectName} already exists.");

        var profile = await _unitOfWork.LegalPersonProfileRepository.FindByIdAsync(dto.ProfileId, cancellationToken);

        if (profile is null)
            throw new InvalidOperationException("No profile with this id found.");

        var images = await Task.WhenAll(dto.Images.Select(async i =>
        {
            return await _fileService.SaveFileAsync(i.Stream, i.Format.Extension);
        }));

        var offerAgreement = await _fileService.SaveFileAsync(dto.OfferAgreement.Stream, dto.OfferAgreement.Format.Extension);

        var announcement = _mapper.Map<Announcement>((dto, images.AsEnumerable(), offerAgreement));

        try
        {
            await _unitOfWork.AnnouncementRepository.AddAsync(announcement, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            foreach (var image in images)
            {
                await _fileService.DeleteFileAsync(image);
            }

            await _fileService.DeleteFileAsync(offerAgreement);

            throw;
        }
    }
}
