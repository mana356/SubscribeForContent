using SFC_DTO.Post;
using SFC_DTO.UserProfile;

namespace SFC_DTO.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public PostDTO Post { get; set; }
        public CommentDTO ParentComment { get; set; }

        public IEnumerable<UserBasicProfileDTO> LikedByUsers { get; set; }

        public IEnumerable<CommentDTO> ChildComments { get; set; }

        public UserBasicProfileDTO User { get; set; }
    }
}
