using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OnlineBusinessPromotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly IProfessionalService service;

        public ProfessionalController(IProfessionalService service)
        {
            this.service = service;
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
    }
}
