using AutoMapper;
using LilaRent.Application.Dto;
using LilaRent.Domain.Fields;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Queries;


public class GetProfileReservationHandler : IRequestHandler<GetProfileReservationQuery, IEnumerable<ReservationSummaryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;


    public GetProfileReservationHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ReservationSummaryDto>> Handle(GetProfileReservationQuery request, CancellationToken cancellationToken)
    {
        var profileId = request.ProfileId;
        var legalEntityType = request.LegalEntityType;

        var reservations = legalEntityType switch
        {
            LegalEntityType.LegalPerson => await _unitOfWork.LegalPersonProfileRepository.GetReservations(profileId),
            LegalEntityType.Individual => await _unitOfWork.IndividualProfileRepository.GetReservations(profileId),

            _ => throw null
        };

        return reservations.Select(r => new ReservationSummaryDto()
        {
            ProfileName = legalEntityType == LegalEntityType.Individual ? r.Announcement?.RentObjectName : r.Client?.Name,
            Id = r.Id,
            End = r.End,
            Begin = r.Begin
        });
    }
}
