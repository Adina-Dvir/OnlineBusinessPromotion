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
        public async Task<List<int>> DetectSuddenDailySpikesAsync()
        {
            var today = DateTime.Today;
            var from = today.AddDays(-7);

            var clicksPerDay = await _clickRepo.GetClicksPerDayForBusinessesAsync(from, today);
            var trending = new List<int>();

            foreach (var kvp in clicksPerDay)
            {
                var businessId = kvp.Key;
                var dailyClicks = kvp.Value;

                for (int i = 1; i < dailyClicks.Count; i++)
                {
                    var prev = dailyClicks[i - 1];
                    var curr = dailyClicks[i];

                    if (prev == 0) continue;

                    double growth = ((double)(curr - prev) / prev) * 100;
                    if (growth >= 200)
                    {
                        trending.Add(businessId);
                        break;
                    }
                }
            }

            return trending.Distinct().ToList();
        }

        public async Task<List<int>> GetTopTrendingBusinessIdsAsync(Dictionary<int, int> currentWeek, Dictionary<int, int> previousWeek)
        {
            var weeklyTrending = RankTrendingBusinesses(currentWeek, previousWeek);
            var dailyTrending = await DetectSuddenDailySpikesAsync();

            // עסקים חדשים בלי שבוע קודם – נחשב אותם גם
            var newBusinesses = currentWeek
                .Where(kvp => !previousWeek.ContainsKey(kvp.Key) && kvp.Value > 0)
                .Select(kvp => kvp.Key);

            return weeklyTrending
                .Union(dailyTrending)
                .Union(newBusinesses)
                .Distinct()
                .Take(5)
                .ToList();
        }



    }
}


