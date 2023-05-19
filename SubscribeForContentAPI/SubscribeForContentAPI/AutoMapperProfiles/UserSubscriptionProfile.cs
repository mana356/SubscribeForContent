using SFC_DataEntities.Entities;
using AutoMapper;
using SFC_DTO.UserSubscription;

namespace SubscribeForContentAPI.AutoMapperProfiles
{
    public class UserSubscriptionProfile : Profile
    { 
        public UserSubscriptionProfile()
        {
            CreateMap<UserSubscription, UserSubscriptionDTO>();
        }
    }
}
