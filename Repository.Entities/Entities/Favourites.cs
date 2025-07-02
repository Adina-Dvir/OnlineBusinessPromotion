using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    internal class Favourites
    {
        [Key]
        public int FavoriteId { get; set; }

    }
}
