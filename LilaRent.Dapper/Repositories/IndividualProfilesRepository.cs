using Dapper;
using LilaRent.Dapper.SqlQueries;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Fields;
using LilaRent.Domain.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.Dapper.Repositories;


public class IndividualProfileRepository : IIndividualProfileRepository
{
    private readonly string _connectionString;


    public IndividualProfileRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


    public Task AddAsync(IndividualProfile entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IndividualProfile?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IndividualProfile>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Announcement>> GetPrevioslyReservedAnnouncements(Guid individualId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var announcementsDbModels = await connection.QueryAsync<AnnouncementDbModel>(
            IndividualProfileSql.GetPrevioslyReservedAnnouncements,
            new { Id = individualId }
        );

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
                      select a with { Images = announcementImages.OrderBy(i => i.Index).ToList() };

        return announcements;
    }

    public async Task<IEnumerable<Reservation>> GetReservations(Guid profileId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var reservations = await connection.QueryAsync<Reservation>(IndividualProfileSql.GetReservations, new { Id = profileId });

        return reservations;
    }

    public Task<IEnumerable<IndividualProfile>> GetWhereAsync(Expression<Func<IndividualProfile, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IndividualProfile>> PageAllAsync(int skip, int take, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object id, IndividualProfile entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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
