using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : IService<CategoryDto>
    {
        private readonly IRepository<Category> repository;
        private readonly IMapper mapper; 
        public CategoryService(IRepository<Category> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task< CategoryDto> AddItem(CategoryDto item)
        {
            // ממפה את ה-DTO לאובייקט קטגוריה מסוג Entity
            Category c = await repository.AddItem(mapper.Map<CategoryDto, Category>(item));

            // מחזיר את הקטגוריה החדש אחרי השמירה, במבנה DTO
            return mapper.Map<Category, CategoryDto>(c);
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task< List<CategoryDto>> GetAll()
        {
            return await mapper.Map<List<Category>,List<CategoryDto>>( repository.GetAll());
        }

        public async Task< CategoryDto> GetById(int id)
        {
            return await mapper.Map < Category, CategoryDto >( repository.GetById(id));
        }

        public async Task UpdateItem(int id, CategoryDto item)
        {
            await repository.UpdateItem(id, mapper.Map<CategoryDto, Category>(item));
        }
    }
}
