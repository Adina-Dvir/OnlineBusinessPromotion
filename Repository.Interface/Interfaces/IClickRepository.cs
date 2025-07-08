using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IClickRepository
    {

        Task<List<ProfessionalClick>> GetClicksInRangeAsync(DateTime start, DateTime end);
        Task<Dictionary<int, int>> GetClickCountsByDateRangeAsync(DateTime startDate, DateTime endDate);
        List<ProfessionalClick> GetClicksForWeek(int weekOffset);
        Task<Dictionary<int, int>> GetClicksByBusinessAsync(DateTime from, DateTime to);
        Task AddClickAsync(ProfessionalClick click);
        Task<Dictionary<int, List<int>>> GetClicksPerDayForBusinessesAsync(DateTime from, DateTime to);


    }
}
