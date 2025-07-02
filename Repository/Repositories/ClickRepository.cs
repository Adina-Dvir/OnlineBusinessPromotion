using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ClickRepository : IClickRepository
    {
        private readonly IContext _context;

        public ClickRepository(IContext context)
        {
            _context = context;
        }

        public async Task<ProfessionalClick> GetById(int id)
        {
            return await _context.ProfessionalClick.FindAsync(id);
        }

        public async Task<IEnumerable<ProfessionalClick>> GetAll()
        {
            return await _context.ProfessionalClick.ToListAsync();
        }

        public async Task AddItem(ProfessionalClick entity)
        {
            await _context.ProfessionalClick.AddAsync(entity);
            await _context.Save();
        }

        public async Task UpdateItem(int id, ProfessionalClick entity)
        {
            var existing = await _context.ProfessionalClick.FindAsync(id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.Save();
            }
        }

        public async Task DeleteItem(int id)
        {
            var item = await _context.ProfessionalClick.FindAsync(id);
            if (item != null)
            {
                _context.ProfessionalClick.Remove(item);
                await _context.Save();
            }
        }

        public async Task<List<ProfessionalClick>> GetClicksInRangeAsync(DateTime start, DateTime end)
        {
            return await _context.ProfessionalClick
                .Where(c => c.ClickedAt >= start && c.ClickedAt <= end)
                .ToListAsync();
        }

        public List<ProfessionalClick> GetClicksForWeek(int weekOffset)
        {
            throw new NotImplementedException();
        }
    }
}
