using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Common.Dto;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace OnlineBusinessPromotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly IProfessionalService service;
        private readonly IContext context;

        public ProfessionalController(IProfessionalService service, IContext context)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet]
        public async Task<List<ProfessionalsDto>> Get()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ProfessionalsDto> Get(int id)
        {
            return await service.GetById(id);
        }

        [HttpPost]
        [Authorize]
        public async Task<ProfessionalsDto> Post([FromForm] ProfessionalFormDto professionalForm)
        {
            // שמירה במסד נתונים כולל תמונות
            var createdProfessional = await service.AddItem(professionalForm);
            return createdProfessional;
        }

        [HttpGet("byCategory/{categoryId}")]
        public async Task<ActionResult<List<ProfessionalsDto>>> GetByCategory(int categoryId)
        {
            var professionals = await service.GetProfessionalsByCategory(categoryId);

            if (professionals == null || professionals.Count == 0)
                return NotFound("לא נמצאו עסקים לקטגוריה זו");

            return Ok(professionals);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromForm] ProfessionalFormDto professional)
        {
            await service.UpdateItem(id, professional);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteItem(id);
            return NoContent();
        }

        // אם אינך משתמשת בזה, ניתן למחוק
        private async Task<byte[]> GetFileBytes(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        [HttpGet("{professionalId}/images")]
        public async Task<ActionResult<IEnumerable<ProfessionalImageDto>>> GetImagesForProfessional(int professionalId)
        {
            var images = await context.ProfessionalImages
                .Where(img => img.ProfessionalId == professionalId)
                .ToListAsync();

            var result = images.Select(img => new ProfessionalImageDto
            {
                ImageId = img.ImageId,
                FileName = img.FileName,
                ProfessionalId = img.ProfessionalId,
                ImageBase64 = $"data:image/jpeg;base64,{Convert.ToBase64String(img.ImageData)}"
            });

            return Ok(result);
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDetailsDto email)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("businessplusonlinesite@gmail.com", "acld acqp qmju fqat"), // ← פה שימי את הסיסמה שקיבלת
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("businessplusonlinesite@gmail.com"), // ← גם כאן
                    Subject = email.Subject,
                    Body = email.MsgBody,
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(email.Recipient);

                await smtpClient.SendMailAsync(mailMessage);

                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error sending email: {ex.Message}");
            }
        }

        // ⭐️ מתודת הטרנדיים החדשה:
        [HttpGet("trending")]
        public async Task<ActionResult<List<ProfessionalsDto>>> GetTrendingProfessionals()
        {
            var clickRepo = (IClickRepository)HttpContext.RequestServices.GetService(typeof(IClickRepository));
            var professionalRepo = (IRepository<Professionals>)HttpContext.RequestServices.GetService(typeof(IRepository<Professionals>));

            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfLastWeek = startOfWeek.AddDays(-7);

            var currentWeek = await clickRepo.GetClicksByBusinessAsync(startOfWeek, today);
            var previousWeek = await clickRepo.GetClicksByBusinessAsync(startOfLastWeek, startOfWeek);

            var trendingService = new Service.Services.TrendingService(clickRepo, professionalRepo);
            var trendingIds = await trendingService.GetTopTrendingBusinessIdsAsync(currentWeek, previousWeek);
            var trendingProfessionals = await trendingService.GetBusinessesByIdsAsync(trendingIds);

            return Ok(trendingProfessionals);
        }
    }
}
