using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    //ממשק שמתאר את הנתונים
    public interface IContext
    {
        public DbSet<Professionals> Professionals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<EmailDetails> EmailDetails { get; set; }
        public DbSet<Category> Category { get; set; }
        public void Save();
    }
}
