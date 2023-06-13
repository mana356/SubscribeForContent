using SFC_DataEntities.Entities;
using AutoMapper;
using SFC_DTO.Comment;

namespace SubscribeForContentAPI.AutoMapperProfiles
{
    public class CommentProfile : Profile
    { 
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentCreationDTO, Comment>();
        }
    }
}
