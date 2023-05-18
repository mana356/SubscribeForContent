using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataEntities.Entities
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }
        
        [MaxLength(255)]
        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [MaxLength(500)]
        [Required]
        public string Bio { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsACreator { get; set; } = false;

        public int? ProfilePictureId { get; set; }
        
        public int? CoverPictureId { get; set; }

        public FileContent? ProfilePicture { get; set; }

        public FileContent? CoverPicture { get; set; }

        public ICollection<CreatorSubscriptionLevel> SubscriptionLevels { get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }

        public ICollection<UserSubscription> Subscribers { get; set; }
        public ICollection<Comment> UserComments { get; set; }
        public ICollection<Comment> LikedComments { get; set; }

        public ICollection<Post> CreatedPosts { get; set; }
        public ICollection<Post> LikedPosts { get; set; }

    }
}
