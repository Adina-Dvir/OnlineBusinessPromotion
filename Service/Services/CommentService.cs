
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
    public class CommentService : IService<CommentDto>
    {
        // משתנה פרטי ששומר את הריפוזיטורי של היוזרים
        private readonly IRepository<Comment> repository;

        // משתנה פרטי ששומר את ה-Mapper (כלי להמרה בין DTO ל-Entity ולהפך)
        private readonly IMapper mapper;

        // בנאי עם תלות – מקבל את הריפוזיטורי וה-Mapper מבחוץ (הזרקת תלויות)
        public CommentService(IRepository<Comment> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // הוספת יוזר חדש למערכת
        public async Task <CommentDto> AddItem(CommentDto item)
        {
            // ממפה את ה-DTO לאובייקט יוזר מסוג Entity
            Comment c =await repository.AddItem(mapper.Map<CommentDto, Comment>(item));

            // מחזיר את היוזר החדש אחרי השמירה, במבנה DTO
            return mapper.Map<Comment, CommentDto>(c);
        }


        // מחיקת יוזר לפי מזהה
        public async Task DeleteItem(int id)
        {
            await  repository.DeleteItem(id);
        }

        // מחזיר רשימה של כל היוזרים
        public async Task<List<CommentDto> >GetAll()
        {
            return await mapper.Map<List<Comment>, List<CommentDto>>(repository.GetAll());
        }

        // מחזיר יוזר בודד לפי מזהה
        public async Task<CommentDto> GetById(int id)
        {
            return await mapper.Map<Comment, CommentDto>(repository.GetById(id));
        }

        // עדכון פרטי יוזר לפי מזהה
        public async Task UpdateItem(int id, CommentDto item)
        {
            await repository.UpdateItem(id, mapper.Map<CommentDto, Comment>(item));
        }

    }
}
