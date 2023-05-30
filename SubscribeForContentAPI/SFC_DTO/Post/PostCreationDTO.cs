using Microsoft.AspNetCore.Http;
using SFC_DTO.Comment;
using SFC_DTO.CreatorSubscriptionLevel;
using SFC_DTO.FileContent;
using SFC_DTO.UserProfile;
using System.ComponentModel.DataAnnotations;

namespace SFC_DTO.Post
{
    public class PostCreationDTO
    {
        [MaxLength(200)]
        [Required]
        public string Title { get; set; }

        [MaxLength(200)]
        [Required]        
        public string Description { get; set; }

        [MaxLength(1000)]
        public string? Content { get; set; }

        [Required]
        public int CreatorSubscriptionLevelId { get; set; }

        public IList<IFormFile>? FileContents { get; set; } = null;
    }
}
