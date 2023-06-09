using AutoMapper;
using SFC_DTO.UserProfile;

namespace SubscribeForContentAPI.AutoMapperProfiles
{
    public class UserProfile : Profile
    { 
        public UserProfile()
        {
            CreateMap<SFC_DataEntities.Entities.UserProfile, UserProfileDTO>();
            CreateMap<SFC_DataEntities.Entities.UserProfile, UserBasicProfileDTO>();
            CreateMap<UserProfileCreationDTO, SFC_DataEntities.Entities.UserProfile > ();
            CreateMap<UserProfileUpdateDTO, SFC_DataEntities.Entities.UserProfile>();
        }
    }
}
