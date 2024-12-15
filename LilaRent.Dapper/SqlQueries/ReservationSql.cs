using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.Dapper.SqlQueries;


public static class ReservationSql
{
    public const string GetAll = """
        SELECT id as Id
             , client_id as ClientId
             , announcement_id as AnnouncementId
             , begin as Begin
             , "end" as End
             , created_at as CreatedAt
             , changed_at as ChangedAt
          FROM reservations
        """;

    public const string GetByAnnouncementId = $"{GetAll} WHERE announcement_id = @AnnouncementId";

    public const string Add = """
        INSERT INTO reservations(id, client_id, announcement_id, begin, "end", created_at, changed_at)
        VALUES (@Id, @ClientId, @AnnouncementId, @Begin, @End, @CreatedAt, @ChangedAt);
        """;

    public const string Update = """
        UPDATE reservations
           SET id=@Id
             , client_id=@ClientId
             , announcement_id=@AnnouncementId
             , begin=@Begin
             , "end"=@End
             , created_at=@CreatedAt
             , changed_at=@ChangedAt
        """;
}
