namespace LilaRent.Application.Mapping;


public partial class Mapper : AutoMapper.Profile
{
    public Mapper()
    {
        CreateAnnouncementMaps();
        CreateUserMaps();
        CreateProfileMaps();
    }
}
