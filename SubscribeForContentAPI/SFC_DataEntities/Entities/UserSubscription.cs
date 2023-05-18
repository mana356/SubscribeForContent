using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataEntities.Entities
{
    public class UserSubscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SubscriberId { get; set; }

        public int CreatorId { get; set; }

        public UserProfile Subscriber { get; set; }

        public UserProfile Creator { get; set; }

        public int SubscriptionLevelId { get; set; }

        public CreatorSubscriptionLevel SubscriptionLevel {get; set;}

        public bool IsValidSubscription { get; set; }

    }
}
