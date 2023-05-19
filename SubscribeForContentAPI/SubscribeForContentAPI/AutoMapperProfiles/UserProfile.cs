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
        }
    }
}
