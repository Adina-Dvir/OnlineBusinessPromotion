using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public DateTime CommentDate { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }// דירוג
        public string CommentContent { get; set; }// תוכן התגובה
    }
}
