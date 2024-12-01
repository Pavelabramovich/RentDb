using LilaRent.Application.Dto;
using LilaRent.Domain.Entities;
using LilaRent.Domain.Fields;


namespace LilaRent.Application.Mapping;


public partial class Mapper : AutoMapper.Profile
{
    private void CreateProfileMaps()
    {
        CreateMap<(ProfileCreatingDto Dto, string ImagePath), Profile>()
            .ConstructUsing(src => src.Dto.LegalEntityType == LegalEntityType.LegalPerson
                ? new LegalPersonProfile()
                {
                    UserId = default,
                    Name = src.Dto.Name,
                    Phone = src.Dto.Phone,
                    Email = src.Dto.Email,
                    Description = src.Dto.Description,
                    ImagePath = src.ImagePath
                }
                : new IndividualProfile()
                {
                    UserId = default,
                    Name = src.Dto.Name,
                    Phone = src.Dto.Phone,
                    Email = src.Dto.Email,
                    Description = src.Dto.Description,
                    ImagePath = src.ImagePath
                });
    }
}
