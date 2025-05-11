using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepositotry : IRepository<Category>
    {
        private readonly IContext context;
        public CategoryRepositotry()
        {
            this.context = context;
        }
        public CategoryRepositotry(IContext context)
        {
            this.context = context;
        }

        public async Task< Category> AddItem(Category item)
        {
            await this.context.Category.AddAsync(item);
            await this.context.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            await this.context.Category.Remove(await GetById(id));
            await this.context.Save();
        }

        public async Task< List<Category>> GetAll()
        {
            return await this.context.Category.ToListAsync();
        }

        public Task<Category> GetById(int id)
        {
            return await this.context.Category.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public Task UpdateItem(int id, Category item)
        {
            var category = await this.GetById(id);
            category.CategoryDescription = item.CategoryDescription;
            category.CategoryName = item.CategoryName;
            await context.Save();
        }
    }
}
