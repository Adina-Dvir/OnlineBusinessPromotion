using Microsoft.EntityFrameworkCore;
using Repository.Entities.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProfessionalImagesRepository : IRepository<ProfessionalImages>
    {
        private readonly IContext context;

        public ProfessionalImagesRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<ProfessionalImages> AddItem(ProfessionalImages item)
        {
            context.ProfessionalImages.Add(item);
            await context.Save();
            return item;
        }

        public async Task<List<ProfessionalImages>> GetAll()
        {
            return await context.ProfessionalImages.ToListAsync();
        }

        public async Task<ProfessionalImages> GetById(int id)
        {
            return await context.ProfessionalImages.FindAsync(id);
        }

        public async Task UpdateItem(int id, ProfessionalImages item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.Save();
        }

        public async Task DeleteItem(int id)
        {
            var item = await context.ProfessionalImages.FindAsync(id);
            if (item != null)
            {
                context.ProfessionalImages.Remove(item);
                await context.Save();
            }
        }
    }

}
