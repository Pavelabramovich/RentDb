namespace LilaRent.Dapper.SqlQueries;


public static class AnnouncementsSql
{
    public const string GetAll = """
        SELECT id as Id
             , profile_id as ProfileId
             , rent_object_name as RentObjectName
             , description as Description
             , price_value as PriceValue
             , price_time_unit as PriceTimeUnit 
             , is_promoted as IsPromoted
             , open_time as OpenTime
             , close_time as CloseTime
             , break_between_reservation as BreakBetweenReservations
             , min_reservation_time as MinReservationTime
             , max_reservation_time as MaxReservationTime
             , can_clients_change_records as CanClientsChangeRecords
             , can_clients_disable_records as CanClientsDisableRecords
             , use_discount as UseDiscount
             , offer_agreement_path as OfferAgreementPath
             , discount_percentage as DiscountPercentage
             , address as Address
             , max_time_for_discount as MaxTimeForDiscount
             , min_time_for_discount as MinTimeForDiscount
          FROM announcements
        """;

    public const string GetById = $"{GetAll} WHERE id = @Id";

    public const string GetByProfileId = $"{GetAll} WHERE profile_id = @ProfileId";


    public const string GetImagesByAnnouncementId = """
        SELECT image_path as ImagePath
             , announcement_id as AnnouncementId
             , index as Index
          FROM announcement_images
         WHERE announcement_id = @AnnouncementId
        """;

    public const string GetAllImages = """
        SELECT image_path as ImagePath, 
               announcement_id as AnnouncementId, 
               index as Index
          FROM announcement_images;
        """;

    public const string Add = """
        INSERT INTO announcements(
            id, 
            profile_id, 
            rent_object_name, 
            description, 
            price_value, 
            price_time_unit, 
            is_promoted, 
            open_time, 
            close_time, 
            break_between_reservation, 
            min_reservation_time, 
            max_reservation_time, 
            can_clients_change_records, 
            can_clients_disable_records, 
            use_discount, 
            offer_agreement_path, 
            discount_percentage, 
            address, 
            max_time_for_discount, 
            min_time_for_discount)
        VALUES (
            @Id, 
            @ProfileId,
            @RentObjectName, 
            @Description, 
            @PriceValue, 
            @PriceTimeUnit, 
            @IsPromoted, 
            @OpenTime, 
            @CloseTime, 
            @BreakBetweenReservations,
            @MinReservationTime, 
            @MaxReservationTime, 
            @CanClientsChangeRecords, 
            @CanClientsDisableRecords, 
            @UseDiscount, 
            @OfferAgreementPath, 
            @DiscountPercentage, 
            @Address, 
            @MaxTimeForDiscount, 
            @MinTimeForDiscount);
        """;

    public const string AddImage = """
        INSERT INTO announcement_images(image_path, announcement_id, index)
        VALUES (@ImagePath, @AnnouncementId, @Index);
        """;

    public const string Update = """
        UPDATE announcements
           SET id=@Id, 
               profile_id=@ProfileId, 
               rent_object_name=@RentObjectName, 
               description=@Description, 
               price_value=@PriceValue, 
               price_time_unit=@PriceTimeUnit, 
               is_promoted=@IsPromoted, 
               open_time=@OpenTime,
               close_time=@CloseTime, 
               break_between_reservation=@BreakBetweenReservations, 
               min_reservation_time=@MinReservationTime, 
               max_reservation_time=@MaxReservationTime, 
               can_clients_change_records=@CanClientsChangeRecords, 
               can_clients_disable_records=@CanClientsDisableRecords, 
               use_discount=@UseDiscount, 
               offer_agreement_path=@OfferAgreementPath, 
               discount_percentage=@DiscountPercentage, 
               address=@Address, 
               max_time_for_discount=@MaxTimeForDiscount, 
               min_time_for_discount=@MinTimeForDiscount
         WHERE id = @Id;
        """;

    public const string Delete = "DELETE FROM announcements WHERE id = @Id";

    public const string DeleteImages = "DELETE FROM announcement_images WHERE announcement_id = @AnnouncementId";
}
