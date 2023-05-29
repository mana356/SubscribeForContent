using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
