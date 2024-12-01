using LilaRent.Dapper.Repositories;
using LilaRent.Domain.Interfaces;
using Microsoft.Extensions.Configuration;


namespace LilaRent.Dapper;


internal class UnitOfWork : IUnitOfWork
{
    // private protected readonly LilaRentContext _context;


    private readonly Lazy<IAnnouncementRepository> _announcementRepositoryLazy;

    private readonly Lazy<IProfileRepository> _profileRepositoryLazy;
    private readonly Lazy<ILegalPersonProfileRepository> _legalPersonProfileRepositoryLazy;
    private readonly Lazy<IIndividualProfileRepository> _individualProfileRepositoryLazy;

    private readonly Lazy<IUserRepository> _userRepositoryLazy;
    private readonly Lazy<IRefreshTokenRepository> _refreshTokenRepositoryLazy;

    private readonly Lazy<IReservationRepository> _reservationRepositoryLazy;


    protected bool _disposed;


    public UnitOfWork(IConfiguration configuration)
    {
        //_context = context;

        _announcementRepositoryLazy = new(() => new AnnouncementRepository(configuration));

        //_profileRepositoryLazy = new(() => new ProfileRepository(_context));
        //_legalPersonProfileRepositoryLazy = new(() => new LegalPersonProfileRepository(_context));
        //_individualProfileRepositoryLazy = new(() => new IndividualProfileRepository(_context));

        //_userRepositoryLazy = new(() => new UserRepository(_context));
        _refreshTokenRepositoryLazy = new(() => new RefreshTokenRepository(configuration));
    }


    public IAnnouncementRepository AnnouncementRepository
    {
        get => !_disposed
            ? _announcementRepositoryLazy.Value
            : throw new ObjectDisposedException(nameof(UnitOfWork), "UnitOfWork is disposed.");
    }


    public IProfileRepository ProfileRepository
    {
        get => !_disposed
            ? _profileRepositoryLazy.Value
            : throw new ObjectDisposedException(nameof(UnitOfWork), "UnitOfWork is disposed.");
    }

    public ILegalPersonProfileRepository LegalPersonProfileRepository
    {
        get => !_disposed
            ? _legalPersonProfileRepositoryLazy.Value
            : throw new ObjectDisposedException(nameof(UnitOfWork), "UnitOfWork is disposed.");
    }

    public IIndividualProfileRepository IndividualProfileRepository
    {
        get => !_disposed
            ? _individualProfileRepositoryLazy.Value
            : throw new ObjectDisposedException(nameof(UnitOfWork), "UnitOfWork is disposed.");
    }

    public IUserRepository UserRepository
    {
        get => !_disposed
            ? _userRepositoryLazy.Value
            : throw new ObjectDisposedException(nameof(UnitOfWork), "UnitOfWork is disposed.");
    }

    public IRefreshTokenRepository RefreshTokenRepository
    {
        get => !_disposed
            ? _refreshTokenRepositoryLazy.Value
            : throw new ObjectDisposedException(nameof(UnitOfWork), "UnitOfWork is disposed.");
    }


    public IReservationRepository ReservationRepository
    {
        get => !_disposed
            ? _reservationRepositoryLazy.Value
            : throw new ObjectDisposedException(nameof(UnitOfWork), "UnitOfWork is disposed.");
    }


    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        
    }

    public void Dispose()
    {
        
    }
}
