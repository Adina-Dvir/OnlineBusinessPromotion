using Common.Dto;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Repository.Entities;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public static class ExtentionService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IService<ProfessionalsDto>, ProfessionalService>();
            services.AddScoped<IService<UserDto>, UserService>();
            services.AddScoped<IService<CategoryDto>, CategoryService>();
            services.AddScoped<IService<CommentDto>, CommentService>();
            services.AddAutoMapper(typeof(MyMapper));
            return services;
        }
    }
}
