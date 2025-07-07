using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock
{
    public class ClickSeeder
    {
        private readonly Database _context;

        public ClickSeeder(Database context)
        {
            _context = context;
        }

        public async Task SeedClicksAsync()
        {
            // שלב 0: מוחק את כל הקליקים הקיימים
            var existingClicks = _context.ProfessionalClick.ToList();
            if (existingClicks.Any())
            {
                _context.ProfessionalClick.RemoveRange(existingClicks);
                await _context.SaveChangesAsync();
            }

            // שלב 1: מוודא שיש עסקים
            var businesses = _context.Professionals.ToList();
            if (!businesses.Any())
                return;

            var random = new Random();
            var clicks = new List<ProfessionalClick>();

            // בסיס לשבוע נוכחי ושבוע קודם (ראשון בבוקר)
            var thisWeekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var lastWeekStart = thisWeekStart.AddDays(-7);

            foreach (var business in businesses)
            {
                // שבוע שעבר – 5 עד 15 קליקים
                int lastWeekClicks = random.Next(5, 15);
                for (int i = 0; i < lastWeekClicks; i++)
                {
                    clicks.Add(new ProfessionalClick
                    {
                        ProfessionalId = business.ProfessionalId,
                        ClickedAt = lastWeekStart.AddMinutes(i * random.Next(10, 60))
                    });
                }

                // השבוע – 10 עד 30 קליקים (כדי שיהיה "צמיחה")
                int thisWeekClicks = random.Next(10, 30);
                for (int i = 0; i < thisWeekClicks; i++)
                {
                    clicks.Add(new ProfessionalClick
                    {
                        ProfessionalId = business.ProfessionalId,
                        ClickedAt = thisWeekStart.AddMinutes(i * random.Next(10, 60))
                    });
                }
            }

            _context.ProfessionalClick.AddRange(clicks);
            await _context.SaveChangesAsync();
        }
    }
}

