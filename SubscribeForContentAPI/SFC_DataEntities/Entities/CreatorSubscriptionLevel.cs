using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataEntities.Entities
{
    public class CreatorSubscriptionLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string LevelName { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal LevelPrice { get; set; }

        [Required]
        [MaxLength(500)]
        public string LevelDescription { get; set; }

        [Required]
        public int CreatorId { get; set; }

        public UserProfile Creator { get; set; }

        public ICollection<UserSubscription> UserSubscriptions { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
