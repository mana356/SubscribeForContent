using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SFC_DTO.UserProfile
{
    public class UserProfileUpdateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Bio { get; set; }

        public bool IsACreator { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        public IFormFile? CoverPicture { get; set; }

    }
}
