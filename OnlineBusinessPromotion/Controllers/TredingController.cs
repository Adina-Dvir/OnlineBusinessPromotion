

using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Services;

[ApiController]
[Route("api/[controller]")]
public class TrendingController : ControllerBase
{
    private readonly TrendingService _trendingService;
    private readonly IClickRepository _clickRepository;

    public TrendingController(TrendingService trendingService, IClickRepository clickRepository)
    {
        _trendingService = trendingService;
        _clickRepository=clickRepository;
    }

    [HttpGet("top5")]
    public async Task<ActionResult<List<ProfessionalsDto>>> GetTopTrendingBusinesses()
    {
        // שלב 1: הבאת נתוני הקליקים מהשבוע הנוכחי ומהשבוע שעבר
        var currentWeek = await _trendingService.GetClicksForWeekAsync(0);   // 0 = השבוע
        var previousWeek = await _trendingService.GetClicksForWeekAsync(1);  // 1 = שבוע קודם

        // שלב 2: דירוג לפי טרנדיות
        var topBusinessIds = _trendingService.RankTrendingBusinesses(currentWeek, previousWeek);

        // שלב 3: שליפה מה-DB והמרה ל-DTO
        var result = await _trendingService.GetBusinessesByIdsAsync(topBusinessIds);

        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> RegisterClick([FromBody] ClickDto dto)
    {
        var click = new ProfessionalClick
        {
            ProfessionalId = dto.ProfessionalId,
            ClickedAt = DateTime.Now
        };

        await _clickRepository.AddClickAsync(click);
        return Ok(new { success = true });
    }
}



