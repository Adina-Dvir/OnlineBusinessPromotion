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

        public Category AddItem(Category item)
        {
            this.context.Category.Add(item);
            this.context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            this.context.Category.Remove(GetById(id));
            this.context.Save();
        }

        public List<Category> GetAll()
        {
            return this.context.Category.ToList();
        }

        public Category GetById(int id)
        {
            return this.context.Category.FirstOrDefault(x => x.CategoryId == id);
        }

        public void UpdateItem(int id, Category item)
        {
            var category = this.GetById(id);
            category.CategoryDescription = item.CategoryDescription;
            category.CategoryName = item.CategoryName;
            context.Save();
        }
    }
}
