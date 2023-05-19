using SFC_DataEntities.Entities;
using AutoMapper;
using SFC_DTO.UserSubscription;
using SFC_DTO.FileContent;

namespace SubscribeForContentAPI.AutoMapperProfiles
{
    public class FileContentProfile : Profile
    { 
        public FileContentProfile()
        {
            CreateMap<FileContent, FileContentDTO>();
        }
    }
}
