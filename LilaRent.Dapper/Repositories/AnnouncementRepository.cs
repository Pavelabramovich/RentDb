using Dapper;
using LilaRent.Dapper.SqlQueries;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Fields;
using LilaRent.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq.Expressions;


namespace LilaRent.Dapper.Repositories;


public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly string _connectionString;


    public AnnouncementRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    public async Task AddAsync(Announcement entity, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var addAnnouncement = new
        {
            entity.Id,
            entity.ProfileId,
            entity.RentObjectName,
            entity.Description,
            PriceValue = entity.Price.Value,
            PriceTimeUnit = entity.Price.TimeUnit,
            entity.IsPromoted,
            entity.OpenTime,
            entity.CloseTime,
            entity.BreakBetweenReservations,
            entity.MinReservationTime,
            entity.MaxReservationTime,
            entity.CanClientsChangeRecords,
            entity.CanClientsDisableRecords,
            entity.UseDiscount,
            entity.OfferAgreementPath,
            entity.DiscountPercentage,
            entity.Address,
            entity.MaxTimeForDiscount,
            entity.MinTimeForDiscount
        };

        await connection.ExecuteAsync(AnnouncementsSql.Add, addAnnouncement);

        foreach (var image in entity.Images)
        {
            await connection.ExecuteAsync(AnnouncementsSql.AddImage, image);
        }
    }

    public Task<Announcement?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Announcement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var announcements = await connection.QueryAsync<Announcement>(AnnouncementsSql.GetAll);

        return announcements;
    }

    public async Task<IEnumerable<Announcement>> GetAllWithImagesAsync(CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var announcementsDbModels = await connection.QueryAsync<AnnouncementDbModel>(AnnouncementsSql.GetAll);

        var announcements = announcementsDbModels.Select(announcementDbModel => new Announcement()
        { 
            Id = announcementDbModel.Id,
            ProfileId = announcementDbModel.ProfileId,
            RentObjectName = announcementDbModel.RentObjectName,
            Address = announcementDbModel.Address,
            Description = announcementDbModel.Description,
            OfferAgreementPath = announcementDbModel.OfferAgreementPath,
            OpenTime = announcementDbModel.OpenTime,
            CloseTime = announcementDbModel.CloseTime,
            BreakBetweenReservations = announcementDbModel.BreakBetweenReservations,
            CanClientsChangeRecords = announcementDbModel.CanClientsChangeRecords,
            CanClientsDisableRecords = announcementDbModel.CanClientsDisableRecords,
            Price = new Price()
            {
                Value = announcementDbModel.PriceValue,
                TimeUnit = announcementDbModel.PriceTimeUnit,
            },
            UseDiscount = announcementDbModel.UseDiscount,
            IsPromoted = announcementDbModel.IsPromoted,
        });


        var images = await connection.QueryAsync<AnnouncementImage>(AnnouncementsSql.GetAllImages);

        announcements = from a in announcements
                               join i in images
                                 on a.Id equals i.AnnouncementId into announcementImages
                             select a with { Images = announcementImages.ToList() };

        return announcements;
    }

    public async Task<Announcement?> GetByIdWithImagesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var announcements = await connection.QueryAsync<AnnouncementDbModel>(AnnouncementsSql.GetById, new { Id = id });
        var announcementDbModel = announcements.FirstOrDefault();

        if (announcementDbModel is null)
            return null;

        var announcement = new Announcement()
        {
            Id = announcementDbModel.Id,
            ProfileId = announcementDbModel.ProfileId,
            RentObjectName = announcementDbModel.RentObjectName,
            Address = announcementDbModel.Address,
            Description = announcementDbModel.Description,
            OfferAgreementPath = announcementDbModel.OfferAgreementPath,
            OpenTime = announcementDbModel.OpenTime,
            CloseTime = announcementDbModel.CloseTime,
            BreakBetweenReservations = announcementDbModel.BreakBetweenReservations,
            CanClientsChangeRecords = announcementDbModel.CanClientsChangeRecords,
            CanClientsDisableRecords = announcementDbModel.CanClientsDisableRecords,
            Price = new Price() 
            { 
                Value = announcementDbModel.PriceValue,
                TimeUnit = announcementDbModel.PriceTimeUnit,
            },
            UseDiscount = announcementDbModel.UseDiscount,
            IsPromoted = announcementDbModel.IsPromoted,
        };

        var images = await connection.QueryAsync<AnnouncementImage>(AnnouncementsSql.GetImagesByAnnouncementId, new { AnnouncementId = announcement.Id });

        announcement = announcement with { Images = images.ToList() };

        return announcement;
    }

    public async Task<IEnumerable<Announcement>> GetByProfileIdAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var announcementsDbModels = await connection.QueryAsync<AnnouncementDbModel>(AnnouncementsSql.GetByProfileId, new { ProfileId = profileId });

        var announcements = announcementsDbModels.Select(announcementDbModel => new Announcement()
        {
            Id = announcementDbModel.Id,
            ProfileId = announcementDbModel.ProfileId,
            RentObjectName = announcementDbModel.RentObjectName,
            Address = announcementDbModel.Address,
            Description = announcementDbModel.Description,
            OfferAgreementPath = announcementDbModel.OfferAgreementPath,
            OpenTime = announcementDbModel.OpenTime,
            CloseTime = announcementDbModel.CloseTime,
            BreakBetweenReservations = announcementDbModel.BreakBetweenReservations,
            CanClientsChangeRecords = announcementDbModel.CanClientsChangeRecords,
            CanClientsDisableRecords = announcementDbModel.CanClientsDisableRecords,
            Price = new Price()
            {
                Value = announcementDbModel.PriceValue,
                TimeUnit = announcementDbModel.PriceTimeUnit,
            },
            UseDiscount = announcementDbModel.UseDiscount,
            IsPromoted = announcementDbModel.IsPromoted,
        });

        var legalPersons = await connection.QueryAsync<LegalPersonProfile>(LegalPersonProfileSql.GetById, new { Id = profileId });
        var legalPerson = legalPersons.First();

        var images = await connection.QueryAsync<AnnouncementImage>(AnnouncementsSql.GetAllImages);

        announcements = from a in announcements
                        join i in images
                          on a.Id equals i.AnnouncementId into announcementImages
                      select a with { Images = announcementImages.OrderBy(i => i.Index).ToList(), Profile = legalPerson };
        
        return announcements;
    }

    public Task<IEnumerable<Announcement>> GetWhereAsync(Expression<Func<Announcement, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Announcement>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        await connection.ExecuteAsync(AnnouncementsSql.Delete, new { Id = (Guid)id });
    }

    public Task UpdateAsync(object id, Announcement entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateWithImagesAsync(Guid id, Announcement entity, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var addAnnouncement = new
        {
            entity.Id,
            entity.ProfileId,
            entity.RentObjectName,
            entity.Description,
            PriceValue = entity.Price.Value,
            PriceTimeUnit = entity.Price.TimeUnit,
            entity.IsPromoted,
            entity.OpenTime,
            entity.CloseTime,
            entity.BreakBetweenReservations,
            entity.MinReservationTime,
            entity.MaxReservationTime,
            entity.CanClientsChangeRecords,
            entity.CanClientsDisableRecords,
            entity.UseDiscount,
            entity.OfferAgreementPath,
            entity.DiscountPercentage,
            entity.Address,
            entity.MaxTimeForDiscount,
            entity.MinTimeForDiscount
        };

        await connection.ExecuteAsync(AnnouncementsSql.Update, addAnnouncement);

        await connection.ExecuteAsync(AnnouncementsSql.DeleteImages, new { AnnouncementId = id });


        foreach (var image in entity.Images)
        {
            await connection.ExecuteAsync(AnnouncementsSql.AddImage, image);
        }
    }



    private record AnnouncementDbModel
    {
        public virtual Guid Id { get; init; } = Guid.NewGuid();

        public virtual required Guid ProfileId { get; init; }
        public virtual LegalPersonProfile? Profile { get; init; }

        public virtual required string RentObjectName { get; set; }
        public virtual required string Address { get; set; }
        public virtual required string Description { get; set; }

        public virtual required string OfferAgreementPath { get; init; }

        public virtual required TimeOnly OpenTime { get; set; }
        public virtual required TimeOnly CloseTime { get; set; }
        public virtual required TimeSpan BreakBetweenReservations { get; set; }

        public virtual required bool CanClientsChangeRecords { get; set; }
        public virtual required bool CanClientsDisableRecords { get; set; }

        public virtual TimeSpan? MinReservationTime { get; set; }
        public virtual TimeSpan? MaxReservationTime { get; set; }
        public required decimal PriceValue { get; init; }
        public required TimeUnit PriceTimeUnit { get; init; }
        public virtual required bool UseDiscount { get; set; }
        public virtual required bool IsPromoted { get; set; }

        public virtual TimeSpan? MinTimeForDiscount { get; set; }
        public virtual TimeSpan? MaxTimeForDiscount { get; set; }
        public virtual int? DiscountPercentage { get; set; }
    }
}
