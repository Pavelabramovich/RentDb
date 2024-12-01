using LilaRent.Application.Dto;
using LilaRent.Domain.Entities;


namespace LilaRent.Application.Mapping;


public partial class Mapper : AutoMapper.Profile
{
    private void CreateUserMaps()
    {
        CreateMap<UserWithProfileCreatingDto, User>()
            .ConstructUsing(src => new User()
            {
                Login = src.Login,
                HashedPassword = default,
                Salt = default
            });
    }
}
