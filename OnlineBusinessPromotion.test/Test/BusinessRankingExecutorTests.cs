using System.Collections.Generic;
using Xunit;
using Service.Logic;

namespace LogicTests
{
    public class BusinessRankingExecutorTests
    {
        [Fact]
        public void ExecuteFullRanking_ShouldReturnTop5TrendingBusinesses()
        {
            // בע"ה שלב א – קליקים השבוע
            var currentWeek = new Dictionary<int, int>
            {
                { 1, 200 }, // עלה ב-150
                { 2, 100 }, // עלה ב-80
                { 3, 120 }, // עלה ב-70
                { 4, 150 }, // עלה ב-60
                { 5, 90 },  // עלה ב-50
                { 6, 70 },  // עלה ב-10 בלבד → לא ייכנס
                { 7, 30 }   // ירד → גם לא ייכנס
            };

            var previousWeek = new Dictionary<int, int>
            {
                { 1, 50 },
                { 2, 20 },
                { 3, 50 },
                { 4, 90 },
                { 5, 40 },
                { 6, 60 },
                { 7, 50 }
            };

            // בעזרת ה' – הפעלת הדירוג
            var executor = new BusinessRankingExecutor();
            var result = executor.ExecuteFullRanking(currentWeek, previousWeek);

            // ציפייה: 5 הכי טרנדיים לפי העלייה
            var expected = new List<int> { 1, 2, 3, 4, 5 };

            Assert.Equal(expected, result);
        }
    }
}
