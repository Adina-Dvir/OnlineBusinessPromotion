using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBusinessPromotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IService<CategoryDto> service;
        public CategoryController(IService<CategoryDto>service)
        {
            this.service = service;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public List<CategoryDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public CategoryDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public CategoryDto Post([FromBody] CategoryDto category)
        {
           return service.AddItem(category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CategoryDto category)
        {
            service.UpdateItem(id, category);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
