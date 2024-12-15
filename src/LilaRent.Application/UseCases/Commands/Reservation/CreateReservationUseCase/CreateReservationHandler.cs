using AutoMapper;
using LilaRent.Application.Exceptions;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.UseCases.Commands;


internal class CreateReservationHandler : IRequestHandler<CreateReservationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public CreateReservationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto ?? throw null;

        if (dto.End < dto.Begin)
            throw null;

        var announcementReservations = await _unitOfWork.ReservationRepository.GetWhereAsync(r => r.AnnouncementId == dto.AnnouncementId);


        if (announcementReservations.Any(r => dto.Begin < r.End && dto.End > r.Begin))
        {
            throw new Exception("This reservation is intersect with other");
        }

        var newReservation = new Reservation()
        {
            ClientId = dto.ClientId,
            AnnouncementId = dto.AnnouncementId,
            Begin = dto.Begin.ToUniversalTime(),
            End = dto.End.ToUniversalTime(),
            CreatedAt = dto.CreatedAt.ToUniversalTime(),
        };

        await _unitOfWork.ReservationRepository.AddAsync(newReservation);

        await _unitOfWork.SaveChangesAsync();
    }
}
