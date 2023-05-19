using SFC_DataEntities.Entities;
using SFC_DTO.Post;
using AutoMapper;

namespace SubscribeForContentAPI.AutoMapperProfiles
{
    public class PostProfile : Profile
    { 
        public PostProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<Post, PostSummaryDTO>()
                .ForMember((m)=> m.LikedByUsersCount, src => src.MapFrom(m => m.LikedByUsers != null ? m.LikedByUsers.Count() : 0))
                .ForMember((m) => m.PostCommentsCount, src => src.MapFrom(m => m.PostComments != null ? m.PostComments.Count() : 0));
            CreateMap<PostCreationDTO, Post>()
                .ForMember((m) => m.FileContents, src => src.Ignore());
        }
    }
}
