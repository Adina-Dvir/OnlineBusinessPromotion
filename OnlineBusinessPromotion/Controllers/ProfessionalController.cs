using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBusinessPromotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
        //הקונטרולר אחראי על ניהול נקודות הקצה
        //(endpoints) של ה-API ומתווך בין הלקוח ללוגיקה העסקית
    {
        private readonly IService<ProfessionalsDto> service;
        public ProfessionalController(IService<ProfessionalsDto> service)
        {
            this.service = service;
        }
        // GET: api/<ProfessionalController>
        [HttpGet]
        public async Task< List<ProfessionalsDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<ProfessionalController>/5
        [HttpGet("{id}")]
        public async Task<ProfessionalsDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<ProfessionalController>
        [HttpPost]
        [Authorize]
        public async Task <ProfessionalsDto> Post([FromForm] ProfessionalsDto professional)
        {
            await UploadImage(professional.fileImage);

            return await service.AddItem(professional);

        }

        // עדכון מקצוען לפי מזהה
        [HttpPut("{id}")]
        [Authorize]
        public async Task Put(int id, [FromForm] ProfessionalsDto professional)
        {
            await UploadImage(professional.fileImage); // במידה והמשתמש משנה תמונה
            await service.UpdateItem(id, professional);
        }

        // מחיקה לפי מזהה
        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            await service.DeleteItem(id);
        }
        private async Task UploadImage(IFormFile file)
        {
            //ניתוב לתמונה
            var path = await Path.Combine(Environment.CurrentDirectory, "Images/", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {

                file.CopyTo(stream);
            }
        }
    }
}
