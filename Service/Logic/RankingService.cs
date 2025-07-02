using System.Collections.Generic;

namespace Service.Logic
{
    public class RankingService
    {
        public Dictionary<int, double> CalculateBusinessRanking(List<int> trendingBusinesses)
        {
            var rankings = new Dictionary<int, double>();
            foreach (var businessId in trendingBusinesses)
            {
                rankings[businessId] = 1.0; // כרגע ניקוד אחיד, כולם טרנדיים באותה מידה
            }
            return rankings;
        }
    }
}
