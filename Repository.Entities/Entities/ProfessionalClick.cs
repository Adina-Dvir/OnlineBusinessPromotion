using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class ProfessionalClick
    {

            public int Id { get; set; }
            public int ProfessionalId { get; set; }
            public DateTime ClickedAt { get; set; }
            public Professionals professional { get; set; } // 💡 Navigation property

            // Navigation (אם יש לך טבלת Businesses):
            // public Business Business { get; set; }
        
    }
}

