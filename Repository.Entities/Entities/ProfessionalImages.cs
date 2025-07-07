using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.Entities
{
    public class ProfessionalImages
    {
        [Key]
        public int ImageId { get; set; }

        public byte[] ImageData { get; set; }

        public string FileName { get; set; } = string.Empty;

        public int ProfessionalId { get; set; }

        [ForeignKey(nameof(ProfessionalId))]
        public Professionals Professional { get; set; }
    }



}
