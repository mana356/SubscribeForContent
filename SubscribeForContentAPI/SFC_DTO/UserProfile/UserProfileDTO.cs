using SFC_DTO.Comment;
using SFC_DTO.CreatorSubscriptionLevel;
using SFC_DTO.FileContent;
using SFC_DTO.Post;
using SFC_DTO.UserSubscription;

namespace SFC_DTO.UserProfile
{
    public class UserProfileDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FirebaseUserId { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Bio { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsACreator { get; set; }

        public FileContentDTO? ProfilePicture { get; set; }

        public FileContentDTO? CoverPicture { get; set; }

        public IEnumerable<CreatorSubscriptionLevelDTO> SubscriptionLevels { get; set; }
        public IEnumerable<UserSubscriptionDTO> Subscriptions { get; set; }

        public IEnumerable<UserSubscriptionDTO> Subscribers { get; set; }
        public IEnumerable<CommentDTO> UserComments { get; set; }
        public IEnumerable<CommentDTO> LikedComments { get; set; }

        public IEnumerable<PostDTO> CreatedPosts { get; set; }
        public IEnumerable<PostDTO> LikedPosts { get; set; }
    }
}
