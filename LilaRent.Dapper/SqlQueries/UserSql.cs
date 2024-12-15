using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.Dapper.SqlQueries;


public static class UserSql
{
    public const string GetByLogin = """
        SELECT u.id as Id
             , u.login as Login
             , u.hashed_password as HashedPassword
             , u.salt as Salt 
          FROM users u
         WHERE u.login = @Login
        """;

    public const string Add = """
        INSERT INTO users(id, login, hashed_password, salt)
        VALUES (@Id, @Login, @HashedPassword, @Salt);
        """;
}
