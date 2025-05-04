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
        public List<ProfessionalsDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<ProfessionalController>/5
        [HttpGet("{id}")]
        public ProfessionalsDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<ProfessionalController>
        [HttpPost]
        [Authorize]
        public ProfessionalsDto Post([FromForm] ProfessionalsDto professional)
        {
            UploadImage(professional.fileImage);

            return service.AddItem(professional);

        }

        // עדכון מקצוען לפי מזהה
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromForm] ProfessionalsDto professional)
        {
            UploadImage(professional.fileImage); // במידה והמשתמש משנה תמונה
            service.UpdateItem(id, professional);
        }

        // מחיקה לפי מזהה
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
        private void UploadImage(IFormFile file)
        {
            //ניתוב לתמונה
            var path = Path.Combine(Environment.CurrentDirectory, "Images/", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {

                file.CopyTo(stream);
            }
        }
    }
}
