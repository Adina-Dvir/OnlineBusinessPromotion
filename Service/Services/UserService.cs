// שימוש ב-AutoMapper למיפוי בין DTO לאובייקטים מה-DB
using AutoMapper;

// שימוש באובייקטים שמייצגים מידע שמועבר הלאה (Data Transfer Objects)
using Common.Dto;

// ישות יוזר מתוך שכבת הישויות (Entity)
using Repository.Entities;

// ממשק של ריפוזיטורי (גישה לדאטה)
using Repository.Interfaces;

// ממשק לשכבת שירות (Service)
using Service.Interfaces;

// שימושים בסיסיים בספריות עזר של .NET
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    // מימוש של שירות ליוזרים, שמממש את הממשק IService
    public class UserService : IService<UserDto>
    {
        // משתנה פרטי ששומר את הריפוזיטורי של היוזרים
        private readonly IRepository<User> repository;

        // משתנה פרטי ששומר את ה-Mapper (כלי להמרה בין DTO ל-Entity ולהפך)
        private readonly IMapper mapper;

        // בנאי עם תלות – מקבל את הריפוזיטורי וה-Mapper מבחוץ (הזרקת תלויות)
        public UserService(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // הוספת יוזר חדש למערכת
        public async Task<UserDto> AddItem(UserDto item)
        {
            // ממפה את ה-DTO לאובייקט יוזר מסוג Entity
            User u = repository.AddItem(mapper.Map<UserDto, User>(item));

            // מחזיר את היוזר החדש אחרי השמירה, במבנה DTO
            return mapper.Map<User, UserDto>(u);
        }

        // מחיקת יוזר לפי מזהה
        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        // מחזיר רשימה של כל היוזרים
        public async Task< List<UserDto> >GetAll()
        {
            return await mapper.Map<List<User>, List<UserDto>>(repository.GetAll());
        }

        // מחזיר יוזר בודד לפי מזהה
        public async Task <UserDto> GetById(int id)
        {
            return await mapper.Map<User, UserDto>(repository.GetById(id));
        }

        // עדכון פרטי יוזר לפי מזהה
        public async Task UpdateItem(int id, UserDto item)
        {
            await repository.UpdateItem(id, mapper.Map<UserDto, User>(item));
        }
    }
}
