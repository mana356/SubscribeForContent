using System.ComponentModel.DataAnnotations;

namespace SFC_DTO.Comment
{
    public class CommentCreationDTO
    {
        [MaxLength(500)]
        [Required]
        public string Body { get; set; }

        [Required]        
        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
