using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ProfessionalImageDto
    {
        public int ImageId { get; set; }  // אפשרי להשאיר לא חייב בשלב ההוספה

        public int ProfessionalId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public byte[] ImageData { get; set; } = Array.Empty<byte>();
    }
}

