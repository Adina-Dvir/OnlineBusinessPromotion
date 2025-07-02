using Microsoft.AspNetCore.Mvc;
using Service.Logic;
using System.Collections.Generic;
using Repository.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrendingController : ControllerBase
    {
        private readonly IClickRepository _clickRepository;

        public TrendingController(IClickRepository clickRepository)
        {
            _clickRepository = clickRepository;
        }

        [HttpGet("top5")]
        public ActionResult<List<int>> GetTop5TrendingBusinesses()
        {
            // שולפים הקלקות גולמיות
            var currentWeekClicksRaw = _clickRepository.GetClicksForWeek(weekOffset: 0);
            var previousWeekClicksRaw = _clickRepository.GetClicksForWeek(weekOffset: 1);

            // מקבצים לפי ProfessionalId וסופרים
            var currentWeekClicks = currentWeekClicksRaw
                .GroupBy(c => c.ProfessionalId)
                .ToDictionary(g => g.Key, g => g.Count());

            var previousWeekClicks = previousWeekClicksRaw
                .GroupBy(c => c.ProfessionalId)
                .ToDictionary(g => g.Key, g => g.Count());

            // מריץ את האלגוריתם
            var executor = new BusinessRankingExecutor();
            var top5 = executor.ExecuteFullRanking(currentWeekClicks, previousWeekClicks);

            return Ok(top5);
        }

    }
}
