using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataEntities.Entities
{
    public class FileContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        public string BlobId { get; set; }

        [Required]
        public string ContainerName { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
