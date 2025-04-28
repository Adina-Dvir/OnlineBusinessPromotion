using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IContext context;
        public UserRepository(IContext context)
        {
            this.context = context;
        }
        public User AddItem(User item)
        {
            this.context.Users.Add(item);
            this.context.Save();
            return item;
        }

        public void DeleteItem(int id)
        {
            this.context.Users.Remove(GetById(id));
            this.context.Save();
        }

        public List<User> GetAll()
        {
            return this.context.Users.ToList();
        }

        public User GetById(int id)
        {
            return this.context.Users.FirstOrDefault(x=>x.UserId==id);
        }

        public void UpdateItem(int id, User item)
        {
            var user = this.GetById(id);
            user.UserName = item.UserName;
            user.UserPassword = item.UserPassword;
            user.UserEmail = item.UserEmail;
            context.Save();
        }
    }
}
