using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataEntities.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Body { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime? UpdatedOn { get; set; }
        public Post Post { get; set; }
        public Comment? ParentComment { get; set; }

        public ICollection<UserProfile> LikedByUsers { get; set; }

        public ICollection<Comment> ChildComments { get; set; }

        public UserProfile User { get; set; }

    }
}
