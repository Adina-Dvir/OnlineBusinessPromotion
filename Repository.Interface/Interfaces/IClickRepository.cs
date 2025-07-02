using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IClickRepository
    {
        Task<ProfessionalClick> GetById(int id);
        Task<IEnumerable<ProfessionalClick>> GetAll();
        Task AddItem(ProfessionalClick entity);
        Task UpdateItem(int id, ProfessionalClick entity);
        Task DeleteItem(int id);
        Task<List<ProfessionalClick>> GetClicksInRangeAsync(DateTime start, DateTime end);
        List<ProfessionalClick> GetClicksForWeek(int weekOffset);
    }
}
