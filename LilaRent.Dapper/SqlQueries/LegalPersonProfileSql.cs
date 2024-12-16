using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.Dapper.SqlQueries;


public static class LegalPersonProfileSql
{
    public const string GetById = """
        SELECT p.id as Id
             , p.user_id as UserId
             , p.name as Name
             , p.description as Description
             , p.image_path as ImagePath
             , p.email as Email
             , p.phone as Phone
             , lp.address as Address
             , lp.floor as Floor
             , lp.station as Station
             , lp.area as Area
          FROM profiles p
               JOIN legal_person_profiles lp
                 ON lp.id = p.id AND lp.id = @Id
        """;

    public const string GetReservations = """
        SELECT id as Id
             , client_id as ClientId
             , announcement_id as AnnouncementId
             , begin as Begin
             , "end" as End
             , created_at as CreatedAt
             , changed_at as ChangedAt
          FROM legal_person_profiles lp
               JOIN announcements a
                 ON lp.id = a.profile_id AND lp.id = @Id
                    JOIN reservations r 
                      ON a.id = r.announcement_id
        """;
}
