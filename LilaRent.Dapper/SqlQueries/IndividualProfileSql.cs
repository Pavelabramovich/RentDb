using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.Dapper.SqlQueries;


public static class IndividualProfileSql
{
    public const string GetPrevioslyReservedAnnouncements = """
        SELECT a.id as Id
             , a.profile_id as ProfileId
             , a.rent_object_name as RentObjectName
             , a.description as Description
             , a.price_value as PriceValue
             , a.price_time_unit as PriceTimeUnit 
             , a.is_promoted as IsPromoted
             , a.open_time as OpenTime
             , a.close_time as CloseTime
             , a.break_between_reservation as BreakBetweenReservations
             , a.min_reservation_time as MinReservationTime
             , a.max_reservation_time as MaxReservationTime
             , a.can_clients_change_records as CanClientsChangeRecords
             , a.can_clients_disable_records as CanClientsDisableRecords
             , a.use_discount as UseDiscount
             , a.offer_agreement_path as OfferAgreementPath
             , a.discount_percentage as DiscountPercentage
             , a.address as Address
             , a.max_time_for_discount as MaxTimeForDiscount
             , a.min_time_for_discount as MinTimeForDiscount 
          FROM individual_profiles ip
               JOIN reservations r 
                 ON ip.id = r.client_id
                    JOIN announcements a
                      ON r.announcement_id = a.id
         WHERE ip.id = @Id
        """;
}
