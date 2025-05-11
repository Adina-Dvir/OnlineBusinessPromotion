﻿using Common.Dto;
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
        public async Task< List<CategoryDto>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task< CategoryDto> Get(int id)
        {
            return await service.GetById(id);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<CategoryDto >Post([FromBody] CategoryDto category)
        {
            return await service.AddItem(category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CategoryDto category)
        {
            await service.UpdateItem(id, category);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await service.DeleteItem(id);
        }
    }
}
