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

        public Professionals AddItem(Professionals item)
        {
            this.context.Professionals.Add(item);
            Console.WriteLine($"City value before saving: {item.City}");
            item.FixNullStrings();
            this.context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            this.context.Professionals.Remove(GetById(id));
            this.context.Save();
        }

        public List<Professionals> GetAll()
        {
            return this.context.Professionals.ToList();
        }

        public Professionals GetById(int id)
        {
            return this.context.Professionals.FirstOrDefault(x => x.ProfessionalId == id);
        }

        public void UpdateItem(int id, Professionals item)
        {
            var professionals = this.GetById(id);
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
