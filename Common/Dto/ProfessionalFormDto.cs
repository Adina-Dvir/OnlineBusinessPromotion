using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

public class ProfessionalFormDto
{
    public int ProfessionalId { get; set; }
    public string? ProfessionalName { get; set; }
    public string? ProfessionalAdress { get; set; }
    public string? ProfessionalDescription { get; set; }
    public string? PriceRange { get; set; }
    public string? ProfessionalPhone { get; set; }
    public string? ProfessionalEmail { get; set; }
    public string? Subject { get; set; }
    public int? Years { get; set; }
    public string? ProfessionalPassword { get; set; }
    public string? ProfessionalPlace { get; set; }
    public string? Profile { get; set; }
    public string? City { get; set; }
    public int? CategoryId { get; set; }

    [NotMapped]
    public IFormFile? fileImage { get; set; }

    [NotMapped]
    public List<IFormFile>? fileImages { get; set; }
}
