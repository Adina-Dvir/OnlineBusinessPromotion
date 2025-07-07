using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Entities;
using Repository.Interfaces;
using Service.Services;

namespace Service.Logic
{
    public class BusinessRankingExecutor
    {
        private readonly TrendingService _trendingService;
        private readonly RankingService _rankingService;

        public BusinessRankingExecutor(
            IClickRepository clickRepo,
            IRepository<Professionals> professionalRepo)
        {
            _trendingService = new TrendingService(clickRepo, professionalRepo);
            _rankingService = new RankingService();
        }

        public List<int> ExecuteFullRanking(Dictionary<int, int> currentWeek, Dictionary<int, int> previousWeek)
        {
            var trendingBusinesses = _trendingService.RankTrendingBusinesses(currentWeek, previousWeek);
            var rankings = _rankingService.CalculateBusinessRanking(trendingBusinesses);

            var sortedBusinessIds = rankings
                .OrderByDescending(kv => kv.Value)
                .Select(kv => kv.Key)
                .Take(5)
                .ToList();

            return sortedBusinessIds;
        }
    }

}
