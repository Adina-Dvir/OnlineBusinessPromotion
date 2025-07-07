using System.Collections.Generic;
using Xunit;
using Moq; // ← חובה
using Repository.Interfaces; // ← כאן נמצא IClickRepository
using Repository.Entities;  // ← כאן נמצא Professionals
using Service.Logic;        // ← כאן נמצא BusinessRankingExecutor

namespace LogicTests
{
    public class BusinessRankingExecutorTests
    {
        [Fact]
        public void ExecuteFullRanking_ShouldReturnTop5TrendingBusinesses()
        {
            var currentWeek = new Dictionary<int, int>
            {
                { 1, 200 },
                { 2, 100 },
                { 3, 120 },
                { 4, 150 },
                { 5, 90 },
                { 6, 70 },
                { 7, 30 }
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

            // 👇 יוצרים Mocks לתלויות
            var mockClickRepo = new Mock<IClickRepository>();
            var mockProfessionalRepo = new Mock<IRepository<Professionals>>();

            // 👇 יוצרים את המחלקה עם התלויות
            var executor = new BusinessRankingExecutor(
                mockClickRepo.Object,
                mockProfessionalRepo.Object
            );

            // הפעלת הפונקציה
            var result = executor.ExecuteFullRanking(currentWeek, previousWeek);

            var expected = new List<int> { 1, 2, 3, 4, 5 };

            Assert.Equal(expected, result);
        }
    }
}
