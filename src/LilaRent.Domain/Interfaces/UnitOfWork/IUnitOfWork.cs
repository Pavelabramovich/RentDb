namespace LilaRent.Domain.Interfaces;


public interface IUnitOfWork : IDisposable
{
    IAnnouncementRepository AnnouncementRepository { get; }

    IProfileRepository ProfileRepository { get; }
    ILegalPersonProfileRepository LegalPersonProfileRepository { get; }
    IIndividualProfileRepository IndividualProfileRepository { get; }

    IUserRepository UserRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }

    IReservationRepository ReservationRepository { get; }


    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
