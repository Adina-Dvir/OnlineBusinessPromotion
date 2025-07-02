using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Services;
using Xunit;

namespace BusinessPromotion.Test
{
    public class TrendingServiceTests
    {
        [Fact]
        public void CalculateTrendingBusinesses_ReturnsCorrectResults()
        {
            // Arrange
            var service = new TrendingService();

            var current = new Dictionary<int, int> {
                { 1, 150 }, // עלייה של 66%
                { 2, 80 },  // עלייה קטנה
                { 3, 10 }   // ירידה
            };

            var previous = new Dictionary<int, int> {
                { 1, 90 },
                { 2, 70 },
                { 3, 20 }
            };

            // Act
            var trending = service.CalculateTrendingBusinesses(current, previous);

            // Assert
            Assert.Contains(1, trending); // ✔
            Assert.DoesNotContain(2, trending); // ✘
            Assert.DoesNotContain(3, trending); // ✘
        }
            [Fact]
            public void RankTrendingBusinesses_ShouldReturnBusinessesRankedByTrend()
            {
                // Arrange – הכנה של נתוני בדיקה
                var service = new TrendingService();

                var previousWeek = new Dictionary<int, int>
            {
                { 1, 100 },
                { 2, 50 },
                { 3, 30 }
            };

                var currentWeek = new Dictionary<int, int>
            {
                { 1, 180 }, // ↑ 80%
                { 2, 70 },  // ↑ 40%
                { 3, 75 }   // ↑ 150%
            };

                // Act – הרצת הפונקציה הנבדקת
                var result = service.RankTrendingBusinesses(currentWeek, previousWeek);

                // Assert – בדיקת התוצאה
                var expected = new List<int> { 3, 1 }; // רק ID 3 ו־1 עלו ב־50% לפחות

                Assert.Equal(expected, result);
            }
        }
    }



