using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ClickService : IClickService
    {
        private readonly IClickRepository _clickRepository;

        public ClickService(IClickRepository clickRepository)
        {
            _clickRepository = clickRepository;
        }

        public async Task<Dictionary<int, int>> GetClicksForCurrentWeekAsync()
        {
            var today = DateTime.UtcNow;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            return await _clickRepository.GetClicksByBusinessAsync(startOfWeek, today);
        }

        public async Task<Dictionary<int, int>> GetClicksForPreviousWeekAsync()
        {
            var today = DateTime.UtcNow;
            var startOfCurrentWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfLastWeek = startOfCurrentWeek.AddDays(-7);
            return await _clickRepository.GetClicksByBusinessAsync(startOfLastWeek, startOfCurrentWeek);
        }
        public async Task<Dictionary<int, List<int>>> GetClicksPerDayForBusinessesAsync(DateTime from, DateTime to)
        {
            return await _clickRepository.GetClicksPerDayForBusinessesAsync(from, to);
        }

    }
}
