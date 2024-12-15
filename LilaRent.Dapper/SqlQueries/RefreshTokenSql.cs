namespace LilaRent.Dapper.SqlQueries;


public static class RefreshTokenSql
{
    public static string GetAll => "SELECT * FROM refresh_tokens";

    public const string Upsert = """
        INSERT INTO refresh_tokens (user_id, value, expires)
        VALUES (@UserId, @Value, @Expires)
        ON CONFLICT (user_id)
        DO UPDATE SET value = @Value, expires = @Expires;
        """;

    public const string Delete = "DELETE FROM refresh_tokens WHERE user_id = @UserId";
}
