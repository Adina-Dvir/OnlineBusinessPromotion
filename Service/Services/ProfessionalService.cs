// שימוש ב-AutoMapper למיפוי בין DTO לאובייקטים מה-DB
using AutoMapper;

// שימוש באובייקטים שמייצגים מידע שמועבר הלאה (Data Transfer Objects)
using Common.Dto;

// ישות מקצועית מתוך שכבת הישויות (Entity)
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
    // מימוש של שירות למקצוענים, שמממש את הממשק IService
    public class ProfessionalService : IService<ProfessionalsDto>
    {
        // משתנה פרטי ששומר את הריפוזיטורי
        private readonly IRepository<Professionals> repository;

        // משתנה פרטי ששומר את ה-Mapper (כלי להמרה בין DTO ל-Entity ולהפך)
        private readonly IMapper mapper;

        // בנאי ריק — לא בשימוש אבל קיים
        public ProfessionalService()
        {
            this.repository = repository; // ⚠️ לא תקין – לא מוזן מבחוץ
            this.mapper = mapper;
        }

        // בנאי עם תלות – מקבל את הריפוזיטורי וה-Mapper מבחוץ (תלוי בהזרקת תלויות)
        public ProfessionalService(IRepository<Professionals> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // הוספת מקצוען חדש למערכת
        public async Task<ProfessionalsDto> AddItem(ProfessionalsDto item)
        {
            // ממפה את ה-DTO לאובייקט ישות
            Professionals p = await repository.AddItem(mapper.Map<ProfessionalsDto, Professionals>(item));

            // מחזיר את האובייקט החדש אחרי שמירה, במבנה DTO
            return await mapper.Map<Professionals, ProfessionalsDto>(p);
        }

        // מחיקת מקצוען לפי מזהה
        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        // מחזיר רשימה של כל המקצוענים
        public async Task<List<ProfessionalsDto>> GetAll()
        {
            return await mapper.Map<List<Professionals>, List<ProfessionalsDto>>(repository.GetAll());
        }

        // מחזיר מקצוען בודד לפי מזהה
        public async Task<ProfessionalsDto> GetById(int id)
        {
            return await mapper.Map<Professionals, ProfessionalsDto>(repository.GetById(id));
        }

        // עדכון מקצוען קיים לפי מזהה
        public async Task UpdateItem(int id, ProfessionalsDto item)
        {
            await repository.UpdateItem(id, mapper.Map<ProfessionalsDto, Professionals>(item));
        }
    }
}
