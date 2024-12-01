using AutoMapper;
using LilaRent.Application.Exceptions;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using MediatR;
using System.Net.Http.Headers;


namespace LilaRent.Application.UseCases.Commands;


internal class DeleteAnnouncementHandler : IRequestHandler<DeleteAnnouncementCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public DeleteAnnouncementHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken = default)
    {
        var id = request.Id;

        var existed = await _unitOfWork.AnnouncementRepository.GetByIdWithImagesAsync(id, cancellationToken) 
            ?? throw new EntityNotFoundException(id);

        foreach (var image in existed.Images)
        {
            await _fileService.DeleteFileAsync(image.ImagePath);
        }

        await _unitOfWork.AnnouncementRepository.RemoveAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
