//using LilaRent.Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;
using LilaRent.Dapper;
using LilaRent.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace LilaRent.Dapper.DependencyInjection;


public static class AddUnitOfWorkExtension
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection @this)
    {
        string connectionString = """
            Server=localhost;
            Port=5432;
            User Id=postgres;
            Password=NotSqlite;
            Database=lila_rent;
            Include Error Detail = true;
            """;


        //@this.AddScoped<LilaRentContext>(serviceProvider =>
        //{


        //    var options = new DbContextOptionsBuilder<LilaRentContext>().UseNpgsql(connectionString).Options;
        //    return new LilaRentContext(options);
        //});

        @this.AddScoped<IUnitOfWork, UnitOfWork>();

        return @this;
    }
}
