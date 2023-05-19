using SFC_DTO.Post;
using SFC_DTO.UserProfile;
using SFC_DTO.UserSubscription;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DTO.CreatorSubscriptionLevel
{
    public class CreatorSubscriptionLevelSummaryDTO
    {
        public int Id { get; set; }

        public string LevelName { get; set; }

        public decimal LevelPrice { get; set; }

        public UserBasicProfileDTO Creator { get; set; }

    }
}
