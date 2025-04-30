using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    //מנהל התלויות עושה שתי דברים יוצר מופע למחלקה ומזריק אותה איפה שצריך?
    public static class ExtentionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Professionals>,ProfessionalsRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepositotry>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            return services;



        }

    }
}
