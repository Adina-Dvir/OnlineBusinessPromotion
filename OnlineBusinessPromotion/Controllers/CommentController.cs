using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBusinessPromotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IService<CommentDto> service;
        public CommentController(IService<CommentDto>service)
        {
            this.service = service;
        }
        // GET: api/<CommentController>
        [HttpGet]
        public List<CommentDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public CommentDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<CommentController>
        [HttpPost]
        public CommentDto Post([FromBody] CommentDto comment)
        {
            return service.AddItem(comment);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CommentDto comment)
        {
            service.UpdateItem(id, comment);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.DeleteItem(id);
        }
    }
}
