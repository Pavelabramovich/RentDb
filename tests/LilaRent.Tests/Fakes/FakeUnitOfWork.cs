using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;


namespace LilaRent.Application.Tests.Fakes;


internal class FakeUnitOfWork : IUnitOfWork
{
    public IAnnouncementRepository AnnouncementRepository { get; }
    public IProfileRepository ProfileRepository { get; }
    public ILegalPersonProfileRepository LegalPersonProfileRepository { get; }
    public IIndividualProfileRepository IndividualProfileRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    public IReservationRepository ReservationRepository { get; }


    public FakeUnitOfWork(
        ICollection<Announcement> announcements,
        ICollection<Profile> profiles,
        ICollection<LegalPersonProfile> legalPersonProfiles,
        ICollection<IndividualProfile> individualProfiles,
        ICollection<User> users,
        ICollection<RefreshToken> refreshTokens,
        ICollection<Reservation> reserations)
    {
        AnnouncementRepository = new FakeAnnouncementsRepository(announcements);
        ProfileRepository = new FakeProfileRepository(profiles);
        LegalPersonProfileRepository = new FakeLegalPersonProfileRepository(legalPersonProfiles);
        IndividualProfileRepository = new FakeIndividualProfileRepository(individualProfiles);
        UserRepository = new FakeUserRepository(users);
        RefreshTokenRepository = new FakeRefreshTokenRepository(refreshTokens);
        ReservationRepository = new FakeReservationRepository(reserations);
    }


    public void Dispose()
    { }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
