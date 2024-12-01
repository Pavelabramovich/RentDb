namespace LilaRent.Dapper.SqlQueries;


public static class RefreshTokenQueries
{
    public static string All => "SELECT * FROM refresh_tokens";

    public static string ContactById => "SELECT * FROM refresh_tokens WHERE id = @TokenId";

    public static string AddContact =>
        @"INSERT INTO refresh_tokens ("", [LastName], [Email], [PhoneNumber])
          VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";

    public static string UpdateContact =>
        @"UPDATE [Contact]
          SET [FirstName] = @FirstName,
              [LastName] = @LastName,
              [Email] = @Email,
              [PhoneNumber] = @PhoneNumber
          WHERE [ContactId] = @ContactId";

    public static string DeleteContact => "DELETE FROM [Contact] WHERE [ContactId] = @ContactId";
}
