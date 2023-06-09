using System.ComponentModel.DataAnnotations;

namespace SFC_DTO.UserProfile
{
    public class UserProfileCreationDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
