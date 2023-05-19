using SFC_DTO.CreatorSubscriptionLevel;
using SFC_DTO.UserProfile;

namespace SFC_DTO.UserSubscription
{
    public class UserSubscriptionDTO
    {
        public int Id { get; set; }

        public UserBasicProfileDTO Subscriber { get; set; }

        public UserBasicProfileDTO Creator { get; set; }

        public CreatorSubscriptionLevelSummaryDTO SubscriptionLevel { get; set; }

        public bool IsValidSubscription { get; set; }

    }
}
