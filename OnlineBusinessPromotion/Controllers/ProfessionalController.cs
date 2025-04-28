using Common.Dto;
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
       
        public ProfessionalsDto Post([FromForm] ProfessionalsDto professional)
        {
            UploadImage(professional.fileImage);

            return service.AddItem(professional);

        }

        // PUT api/<ProfessionalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] string value)
        {
        }

        // DELETE api/<ProfessionalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
