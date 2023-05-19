using SFC_DTO.Comment;
using SFC_DTO.CreatorSubscriptionLevel;
using SFC_DTO.FileContent;
using SFC_DTO.UserProfile;

namespace SFC_DTO.Post
{
    public class PostSummaryDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserBasicProfileDTO Creator { get; set; }

        public CreatorSubscriptionLevelSummaryDTO SubscriptionLevel { get; set; }

        public int LikedByUsersCount { get; set; }

        public int PostCommentsCount { get; set; }

        public IEnumerable<FileContentDTO> FileContents { get; set; }
    }
}
