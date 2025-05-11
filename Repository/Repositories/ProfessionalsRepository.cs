using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProfessionalsRepository : IRepository<Professionals>
    {
        private readonly IContext context;
        public ProfessionalsRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<Professionals >AddItem(Professionals item)
        {
            await this.context.Professionals.AddAsync(item);
            Console.WriteLine($"City value before saving: {item.City}");
            item.FixNullStrings();
            await this.context.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
             this.context.Professionals.Remove(await GetById(id));
            await this.context.Save();
        }

        public async Task<List<Professionals>> GetAll()
        {
            return await this.context.Professionals.ToListAsync();
        }

        public async Task<Professionals> GetById(int id)
        {
            return await this.context.Professionals.FirstOrDefaultAsync(x => x.ProfessionalId == id);
        }

        public async Task UpdateItem(int id, Professionals item)
        {
            var professionals = await this.GetById(id);
            professionals.ProfessionalPhone = item.ProfessionalPhone;
            professionals.ProfessionalPlace = item.ProfessionalPlace;
            professionals.ProfessionalDescription = item.ProfessionalDescription;
            professionals.ProfessionalPassword = item.ProfessionalPassword;
            professionals.ProfessionalAdress = item.ProfessionalAdress;
            professionals.ProfessionalEmail = item.ProfessionalEmail;
            professionals.ProfessionalName = item.ProfessionalName;
            context.Save();
        }
    }
}
