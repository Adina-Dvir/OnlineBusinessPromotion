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
        public async Task<List<CommentDto> >Get()
        {
            return await service.GetAll();
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public async Task<CommentDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<CommentDto> Post([FromBody] CommentDto comment)
        {
            return await service.AddItem(comment);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CommentDto comment)
        {
            await service.UpdateItem(id, comment);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItem(id);
        }
    }
}
