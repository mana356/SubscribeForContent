using SFC_DTO.Post;
using SFC_DTO.UserProfile;
using System.Text.Json.Serialization;

namespace SFC_DTO.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public IEnumerable<UserBasicProfileDTO> LikedByUsers { get; set; }

        public IEnumerable<CommentDTO> ChildComments { get; set; }

        public UserBasicProfileDTO User { get; set; }
    }
}
