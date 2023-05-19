using SFC_DataEntities.Entities;
using AutoMapper;
using SFC_DTO.CreatorSubscriptionLevel;

namespace SubscribeForContentAPI.AutoMapperProfiles
{
    public class CreatorSubscriptionLevelProfile : Profile
    { 
        public CreatorSubscriptionLevelProfile()
        {
            CreateMap<CreatorSubscriptionLevel, CreatorSubscriptionLevelDTO>();
            CreateMap<CreatorSubscriptionLevel, CreatorSubscriptionLevelSummaryDTO>();

        }
    }
}
