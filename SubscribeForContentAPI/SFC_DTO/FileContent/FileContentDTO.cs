using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DTO.FileContent
{
    public class FileContentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Extension { get; set; }

        public string Url { get; set; }

        public int? PostId { get; set; }

        public int? UserProfilePictureId { get; set; }

        public int? UserCoverPictureId { get; set; }

    }
}
