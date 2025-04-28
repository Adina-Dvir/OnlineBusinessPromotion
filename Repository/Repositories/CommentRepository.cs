using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly IContext context;
        public CommentRepository(IContext context) 
        {
            this.context = context;
        }
        public Comment AddItem(Comment item)
        {
            context.Comments.Add(item);
            context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            this.context.Comments.Remove(GetById(id));
            this.context.Save();
        }

        public List<Comment> GetAll()
        {
            return this.context.Comments.ToList();
        }

        public Comment GetById(int id)
        {
            return this.context.Comments.FirstOrDefault(x => x.CommentId == id);
        }

        public void UpdateItem(int id, Comment item)
        {

        }
    }
}
