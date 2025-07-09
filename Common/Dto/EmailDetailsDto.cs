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
            public string Recipient { get; set; } = ""; // כתובת המייל של העסק
            public string Subject { get; set; } = "";   // נושא
            public string MsgBody { get; set; } = "";   // גוף ההודעה
        }
    }


