

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
        var currentWeek = await _trendingService.GetClicksForWeekAsync(0);
        var previousWeek = await _trendingService.GetClicksForWeekAsync(1);

        // קבלת כל העסקים הטרנדיים - כולל קפיצה חדה יומית
        var topIds = await _trendingService.GetTopTrendingBusinessIdsAsync(currentWeek, previousWeek);

        var businesses = await _trendingService.GetBusinessesByIdsAsync(topIds);
        return Ok(businesses);
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



