using Service.Services;
using System.Collections.Generic;

namespace Service.Logic
{
    public class BusinessRankingExecutor
    {
        private readonly TrendingService _trendingService = new();
        private readonly RankingService _rankingService = new();

        public List<int> ExecuteFullRanking(Dictionary<int, int> currentWeek, Dictionary<int, int> previousWeek)
        {
            // שלב א: מחשב את העסקים הכי טרנדיים
            var trendingBusinesses = _trendingService.CalculateTrendingBusinesses(currentWeek, previousWeek);

            // שלב ב: מדירג אותם לפי טרנדיות
            var rankings = _rankingService.CalculateBusinessRanking(trendingBusinesses);

            var sortedBusinessIds = rankings
                .OrderByDescending(kv => kv.Value)
                .Select(kv => kv.Key)
                .Take(5) // ✅ מחזיר רק את 5 הכי טרנדיים
                .ToList();

            return sortedBusinessIds;
        }
    }
}
