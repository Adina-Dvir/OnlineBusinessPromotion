using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto    
{
    public class EmailDetailsDto
    {
        [Key]
        public int EmailId { get; set; }
        public string Mambers { get; set; }//?
        public string Recipient { get; set; }//?
        public string MsgBody { get; set; }//גוף ההודעה
        public string Subject { get; set; }//נושא
        public string Attachment { get; set; }//?
    }
}
