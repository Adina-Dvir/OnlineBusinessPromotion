using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Service.Services;
using Repository.Interfaces;
using Repository.Entities;

namespace BusinessPromotion.Test
{
    public class TrendingServiceTests
    {
        [Fact]
        public void CalculateTrendingBusinesses_ReturnsCorrectResults()
        {
            // Arrange
            var mockClickRepo = new Mock<IClickRepository>();
            var mockProfessionalsRepo = new Mock<IRepository<Professionals>>();
            var service = new TrendingService(mockClickRepo.Object, mockProfessionalsRepo.Object);

            var current = new Dictionary<int, int> {
                { 1, 150 },
                { 2, 80 },
                { 3, 10 }
            };

            var previous = new Dictionary<int, int> {
                { 1, 90 },
                { 2, 70 },
                { 3, 20 }
            };

            // Act
            var trending = service.RankTrendingBusinesses(current, previous);

            // Assert
            Assert.Contains(1, trending);
            Assert.DoesNotContain(2, trending);
            Assert.DoesNotContain(3, trending);
        }

        [Fact]
        public void RankTrendingBusinesses_ShouldReturnBusinessesRankedByTrend()
        {
            // Arrange
            var mockClickRepo = new Mock<IClickRepository>();
            var mockProfessionalsRepo = new Mock<IRepository<Professionals>>();
            var service = new TrendingService(mockClickRepo.Object, mockProfessionalsRepo.Object);

            var previousWeek = new Dictionary<int, int>
            {
                { 1, 100 },
                { 2, 50 },
                { 3, 30 }
            };

            var currentWeek = new Dictionary<int, int>
            {
                { 1, 180 },
                { 2, 70 },
                { 3, 75 }
            };

            // Act
            var result = service.RankTrendingBusinesses(currentWeek, previousWeek);

            // Assert
            var expected = new List<int> { 3, 1 };
            Assert.Equal(expected, result);
        }
    }
}
