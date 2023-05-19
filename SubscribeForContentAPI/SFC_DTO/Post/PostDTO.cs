using SFC_DTO.Comment;
using SFC_DTO.CreatorSubscriptionLevel;
using SFC_DTO.FileContent;
using SFC_DTO.UserProfile;

namespace SFC_DTO.Post
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserBasicProfileDTO Creator { get; set; }

        public CreatorSubscriptionLevelSummaryDTO CreatorSubscriptionLevel { get; set; }

        public IEnumerable<UserBasicProfileDTO> LikedByUsers { get; set; }

        public IEnumerable<CommentDTO> PostComments { get; set; }

        public IEnumerable<FileContentDTO> FileContents { get; set; }
    }
}
