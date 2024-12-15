using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Fields;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public class GetAnnouncementReservationHandler : IRequestHandler<GetAnnouncementReservationQuery, IEnumerable<ReservationSummaryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public GetAnnouncementReservationHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ReservationSummaryDto>> Handle(GetAnnouncementReservationQuery request, CancellationToken cancellationToken)
    {
        var announcementId = request.AnnouncementId;

        var reservations = await _unitOfWork.ReservationRepository.GetAnnouncementReservations(announcementId);

        return reservations.Select(r => new ReservationSummaryDto()
        {
            ProfileName = "NULL",
            Id = r.Id,
            End = r.End,
            Begin = r.Begin
        });
    }
}
