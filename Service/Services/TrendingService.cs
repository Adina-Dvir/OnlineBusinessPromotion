using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Interfaces;
using Common.Dto;
using Repository.Repositories;

namespace Service.Services
{
    public class TrendingService
    {

        private readonly IClickRepository _clickRepo;
        private readonly IRepository<Professionals> _professionalRepo;

        public TrendingService(IClickRepository clickRepo, IRepository<Professionals> professionalRepo)
        {
            _clickRepo = clickRepo;
            _professionalRepo = professionalRepo;
        }

        public async Task<Dictionary<int, int>> GetClicksForWeekAsync(int weekOffset)
        {
            var today = DateTime.Today;
            var currentDayOfWeek = (int)today.DayOfWeek; // 0=Sunday, 6=Saturday
            var startOfWeek = today.AddDays(-currentDayOfWeek - 7 * weekOffset); // ראשון לפני X שבועות
            var endOfWeek = startOfWeek.AddDays(7); // עד שבת

            return await _clickRepo.GetClicksByBusinessAsync(startOfWeek, endOfWeek);
        }


        public List<int> RankTrendingBusinesses(
            Dictionary<int, int> currentWeek,
            Dictionary<int, int> previousWeek)
        {
            return currentWeek
                .Where(kvp => previousWeek.ContainsKey(kvp.Key) && previousWeek[kvp.Key] > 0)
                .Select(kvp => new
                {
                    BusinessId = kvp.Key,
                    Growth = ((double)(kvp.Value - previousWeek[kvp.Key]) / previousWeek[kvp.Key]) * 100
                })
                .Where(x => x.Growth >= 50)
                .OrderByDescending(x => x.Growth)
                .Select(x => x.BusinessId)
                 .Take(5)    // <-- מגבלת 5 עסקים

                .ToList();
        }

        public async Task<List<ProfessionalsDto>> GetBusinessesByIdsAsync(List<int> ids)
        {
            var allProfessionals = await _professionalRepo.GetAll();

            return allProfessionals
                .Where(p => ids.Contains(p.ProfessionalId))
                .Select(p => new ProfessionalsDto
                {
                    ProfessionalId = p.ProfessionalId,
                    ProfessionalName = p.ProfessionalName,
                    ProfessionalEmail = p.ProfessionalEmail,
                    CategoryId = p.CategoryId,
                    ProfessionalDescription = p.ProfessionalDescription
                })
                .ToList();
        }


    }
}


