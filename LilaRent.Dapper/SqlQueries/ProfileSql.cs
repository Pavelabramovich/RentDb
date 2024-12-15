using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.Dapper.SqlQueries;


public static class ProfileSql
{
    public const string GetAll = """
        SELECT p.id as Id
             , p.user_id as UserId
             , p.name as Name
             , p.description as Description
             , p.image_path as ImagePath
             , p.email as Email
             , p.phone as Phone
          FROM profiles p
        """;

    public const string GetAllIndividuals = """
        SELECT p.id as Id
             , p.user_id as UserId
             , p.name as Name
             , p.description as Description
             , p.image_path as ImagePath
             , p.email as Email
             , p.phone as Phone
          FROM profiles p
               JOIN individual_profiles ip
                 ON ip.id = p.id
        """;

    public const string GetAllLegalPersons = """
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
                 ON lp.id = p.id
        """;

    public const string GetById = $"{GetAll} WHERE p.id = @Id";
}
