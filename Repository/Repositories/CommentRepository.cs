using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public  class CommentRepository : IRepository<Comment>
    {
        private readonly IContext context;
        public CommentRepository(IContext context) 
        {
            this.context = context;
        }
        public async Task< Comment > AddItem(Comment item)
        {
            await context.Comments.AddAsync(item);
            await context.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
             this.context.Comments.Remove(await GetById(id));
            await this.context.Save();
        }

        public async Task<List<Comment>> GetAll()
        {
            return await this.context.Comments.ToListAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            return await this.context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
        }

        public async Task UpdateItem(int id, Comment item)
        {
            

        }
    }
}
