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
        public async Task<User >AddItem(User item)
        {
            await this.context.Users.AddAsync(item);
            await this.context.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            await this.context.Users.Remove(GetById(id));
            await this.context.Save();
        }

        public async Task<List<User> >GetAll()
        {
            return await this.context.Users.ToListAsync();
        }

        public async Task<User > GetById(int id)
        {
            return await this.context.Users.FirstOrDefaultAsync(x=>x.UserId==id);
        }

        public async Task UpdateItem(int id, User item)
        {
            var user = await this.GetById(id);
            user.UserName = item.UserName;
            user.UserPassword = item.UserPassword;
            user.UserEmail = item.UserEmail;
            await context.Save();
        }
    }
}
