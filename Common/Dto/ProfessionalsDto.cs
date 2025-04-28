using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Common.Dto
{
    public class ProfessionalsDto
    {
        [Key]
        public int ProfessionalId { get; set; }
        public string? ProfessionalName { get; set; } = "";
        public string? ProfessionalAdress { get; set; } = "";
        public string? ProfessionalDescription { get; set; } = "";
        public string? PriceRange { get; set; } = "";
        public string? ProfessionalPhone { get; set; } = "";
        public string? ProfessionalEmail { get; set; } = "";
        public string? Subject { get; set; } = "";
        public int? Years { get; set; }= 0;
        public string? ProfessionalPassword { get; set; } = "";
        public DateTime? UploadDate { get; set; }= DateTime.Now;
        public byte[]? ArrImage { get; set; }

        public string? ProfessionalPlace { get; set; } = "";
        public string? Profile { get; set; } = "";
        public string? City { get; set; } = "";
        public int? CategoryId { get; set; }
        public IFormFile? fileImage { get; set; }



    }
}
