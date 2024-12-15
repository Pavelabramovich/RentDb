using LilaRent.Application.DependencyInjection;
using LilaRent.Application.Tests.Fakes;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Interfaces;
using MediatR;


namespace LilaRent.Application.Tests.UseCasesTests;


public class BaseUseCaseTest
{
    protected readonly ICollection<Announcement> _announcements;
    protected readonly ICollection<Profile> _profiles;
    protected readonly ICollection<LegalPersonProfile> _legalPersonProfiles;
    protected readonly ICollection<IndividualProfile> _individualProfiles;
    protected readonly ICollection<User> _users;
    protected readonly ICollection<RefreshToken> _refreshTokens;
    protected readonly ICollection<Reservation> _reservations;

    protected readonly string _serverPrefix;
    protected readonly Dictionary<string, Stream> _files;

    protected readonly IMediator _mediator;    


    public BaseUseCaseTest()
    {
        _announcements = new List<Announcement>();
        _profiles = new List<Profile>();
        _legalPersonProfiles = new List<LegalPersonProfile>();
        _individualProfiles = new List<IndividualProfile>();
        _users = new List<User>();
        _refreshTokens = new List<RefreshToken>();
        _reservations = new List<Reservation>();

        _serverPrefix = "storage";
        _files = new() { ["TEST_1"] = new MemoryStream("test123"u8.ToArray()) };


        var fakeUnitOfWork = new FakeUnitOfWork(
            _announcements, 
            _profiles, 
            _legalPersonProfiles, 
            _individualProfiles, 
            _users, 
            _refreshTokens, 
            _reservations
        );

        var fakeFileService = new FakeFileService(_serverPrefix, _files);

        var services = new ServiceCollection()
            .AddUseCases()
            .AddDtoMapper()
            .AddDtoValidators()
            .AddSingleton<IUnitOfWork>(fakeUnitOfWork)
            .AddSingleton<IFileService>(fakeFileService);

        var provider = services.BuildServiceProvider();

        _mediator = provider.GetRequiredService<IMediator>();

        _users.Add(new User()
        {
            Login = "Login",
            HashedPassword = "hashed",
            Salt = "salt",
        });

        _legalPersonProfiles.Add(new LegalPersonProfile()
        {
            UserId = _users.First().Id,
            Name = "name",
            Phone = "+375 (12) 452 5352",
            Email = "email",
            Description = "description",
            ImagePath = "iamge"
        });

        _profiles.Add(_legalPersonProfiles.First());

        _announcements.Add(new Announcement()
        {
            ProfileId = _legalPersonProfiles.First().Id,
            RentObjectName = "ron",
            Address = "address",
            Description = "desc",
            OfferAgreementPath = "off",
            OpenTime = TimeOnly.MinValue,
            CloseTime = TimeOnly.MinValue,
            BreakBetweenReservations = TimeSpan.FromMicroseconds(1249),
            CanClientsChangeRecords = false,
            CanClientsDisableRecords = true,
            Price = new() { TimeUnit = Domain.Fields.TimeUnit.Minutes, Value = 10 },
            UseDiscount = false,
            IsPromoted = false
        });
    }
}
