using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Mock
{
    public class Database : DbContext,IContext
    {
        public DbSet<Professionals> Professionals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<EmailDetails> EmailDetails { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProfessionalClick> ProfessionalClick { get; set; }

        public async Task Save()
        {
           await SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-1VUANBN;database=BusinessDB;trusted_connection=true;TrustServerCertificate=True");
        }
        public new EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }
    }
}
