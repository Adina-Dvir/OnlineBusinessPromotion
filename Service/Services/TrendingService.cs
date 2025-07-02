using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Services; // אם TrendingService מוגדר שם


namespace Service.Services
{
    public class TrendingService
    {
        public List<int> CalculateTrendingBusinesses(
            Dictionary<int, int> currentWeek,
            Dictionary<int, int> previousWeek)
        {
            var trending = new List<int>();

            foreach (var kvp in currentWeek)
            {
                var businessId = kvp.Key;
                var currentClicks = kvp.Value;

                previousWeek.TryGetValue(businessId, out int prevClicks);

                if (prevClicks == 0)
                    continue;

                double percentIncrease = ((double)(currentClicks - prevClicks) / prevClicks) * 100;

                if (percentIncrease >= 50)
                    trending.Add(businessId);
            }

            return trending;
        }


        /// <summary>
        /// מדורג את העסקים לפי אחוז העלייה בכמות הקליקים לעומת השבוע הקודם.
        /// רק עסקים עם עלייה של לפחות 50% נחשבים טרנדיים.
        /// </summary>
        /// <param name="currentWeek">מילון: BusinessID -> קליקים שבוע נוכחי</param>
        /// <param name="previousWeek">מילון: BusinessID -> קליקים שבוע קודם</param>
        /// <returns>רשימה מדורגת של BusinessIDs לפי אחוז עלייה, מהגבוה לנמוך</returns>

        public List<int> RankTrendingBusinesses(
            Dictionary<int, int> currentWeek,
            Dictionary<int, int> previousWeek)
        {
            var trendingGrowth = new List<(int businessId, double growth)>();

            foreach (var kvp in currentWeek)
            {
                var businessId = kvp.Key;
                var currentClicks = kvp.Value;

                previousWeek.TryGetValue(businessId, out int prevClicks);

                if (prevClicks == 0)
                    continue; // אי אפשר לחשב צמיחה אם לא היו קליקים בכלל קודם

                double growth = ((double)(currentClicks - prevClicks) / prevClicks) * 100;

                if (growth >= 50) // נחשב רק אם טרנדי
                    trendingGrowth.Add((businessId, growth));
            }

            // מיון מהכי טרנדי לפחות
            var sorted = trendingGrowth
                .OrderByDescending(b => b.growth)
                .Select(b => b.businessId)
                .ToList();

            return sorted;
        }

    }
}

