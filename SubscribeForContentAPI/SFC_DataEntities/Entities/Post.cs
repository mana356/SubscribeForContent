using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFC_DataEntities.Entities
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(200)]
        [Required]
        public string Title { get; set; }
        
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
        
        [MaxLength(1000)]
        public string? Content { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int CreatorSubscriptionLevelId { get; set; }

        public UserProfile Creator { get; set; }
        
        public CreatorSubscriptionLevel CreatorSubscriptionLevel { get; set; }
        
        public ICollection<UserProfile> LikedByUsers { get; set; }

        public ICollection<Comment> PostComments { get; set; }

        public ICollection<FileContent> FileContents { get; set; }

    }
}