
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ClickRepository : IClickRepository
    {
        private readonly IContext _context;

        public ClickRepository(IContext context)
        {
            _context = context;
        }
        public async Task AddClickAsync(ProfessionalClick click)
        {
            _context.ProfessionalClick.Add(click);
            await _context.Save();
        }


        public async Task<List<ProfessionalClick>> GetClicksInRangeAsync(DateTime start, DateTime end)
        {
            return await _context.ProfessionalClick
                .Where(c => c.ClickedAt >= start && c.ClickedAt <= end)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetClickCountsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.ProfessionalClick
                .Where(click => click.ClickedAt >= startDate && click.ClickedAt <= endDate)
                .GroupBy(click => click.ProfessionalId)
                .Select(group => new
                {
                    ProfessionalId = group.Key,
                    ClickCount = group.Count()
                })
                .ToDictionaryAsync(g => g.ProfessionalId, g => g.ClickCount);
        }

        public async Task<Dictionary<int, int>> GetClicksForWeekAsync(int weekOffset)
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek); // ראשון בבוקר
            var targetWeekStart = startOfWeek.AddDays(-7 * weekOffset);
            var targetWeekEnd = targetWeekStart.AddDays(7);

            return await _context.ProfessionalClick
                .Where(click => click.ClickedAt >= targetWeekStart && click.ClickedAt < targetWeekEnd)
                .GroupBy(click => click.ProfessionalId)
                .Select(group => new
                {
                    ProfessionalId = group.Key,
                    ClickCount = group.Count()
                })
                .ToDictionaryAsync(g => g.ProfessionalId, g => g.ClickCount);
        }

        public List<ProfessionalClick> GetClicksForWeek(int weekOffset)
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var targetWeekStart = startOfWeek.AddDays(-7 * weekOffset);
            var targetWeekEnd = targetWeekStart.AddDays(7);

            return _context.ProfessionalClick
                .Where(click => click.ClickedAt >= targetWeekStart && click.ClickedAt < targetWeekEnd)
                .ToList();
        }

        public async Task<Dictionary<int, int>> GetClicksByBusinessAsync(DateTime from, DateTime to)
        {
            return await _context.ProfessionalClick
                .Where(click => click.ClickedAt >= from && click.ClickedAt <= to)
                .GroupBy(click => click.ProfessionalId)
                .Select(group => new
                {
                    ProfessionalId = group.Key,
                    ClickCount = group.Count()
                })
                .ToDictionaryAsync(g => g.ProfessionalId, g => g.ClickCount);
        }
        public async Task<Dictionary<int, List<int>>> GetClicksPerDayForBusinessesAsync(DateTime from, DateTime to)
        {
            var result = await _context.ProfessionalClick
                .Where(c => c.ClickedAt >= from && c.ClickedAt < to)
                .GroupBy(c => new { c.ProfessionalId, Day = c.ClickedAt.Date })
                .Select(g => new
                {
                    g.Key.ProfessionalId,
                    Day = g.Key.Day,
                    ClickCount = g.Count()
                })
                .ToListAsync();

            var groupedByBusiness = result
                .GroupBy(r => r.ProfessionalId)
                .ToDictionary(
                    g => g.Key,
                    g =>
                    {
                        var dailyCounts = new int[(to - from).Days];
                        foreach (var entry in g)
                        {
                            int dayIndex = (entry.Day - from.Date).Days;
                            if (dayIndex >= 0 && dayIndex < dailyCounts.Length)
                            {
                                dailyCounts[dayIndex] = entry.ClickCount;
                            }
                        }
                        return dailyCounts.ToList();
                    });

            return groupedByBusiness;
        }


    }
}

