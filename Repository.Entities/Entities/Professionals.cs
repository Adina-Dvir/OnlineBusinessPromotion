using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Professionals
    {
        [Key]
        public int ProfessionalId { get; set; }
        public string ProfessionalName { get; set; }
        public string ProfessionalAdress { get; set; }
        public string ProfessionalDescription { get;set; }
        public string? PriceRange { get; set; } = "";
        public string ProfessionalPhone { get; set; }
        public string ProfessionalEmail { get; set; }
        public string Subject { get; set; }
        public int? Years { get; set; }
        public string ProfessionalPassword { get; set; }
        public DateTime UploadDate { get; set; }
        public string ImageUrls { get; set; }
        public string ProfessionalPlace { get; set; }
        public string Profile { get; set; }
        public string City { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
        public void FixNullStrings()
        {
            var stringProperties = this.GetType()
                                        .GetProperties()
                                        .Where(p => p.PropertyType == typeof(string));

            foreach (var prop in stringProperties)
            {
                var value = (string?)prop.GetValue(this);
                if (value == null)
                {
                    prop.SetValue(this, "");
                }
            }
        }
    }
}
