using AutoMapper;
using Common.Dto;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MyMapper : Profile
    {
        //מביא לנו את הניתוב ביחסי של הפרויקט במחשב עד הפרויקט ואז אני צריכה להכניס לו שיכנס לתמונות
        string path = Path.Combine(Environment.CurrentDirectory, "Images\\");

        public MyMapper()
        {

            // 🟡 מיפוי של Professionals ➡️ ProfessionalsDto
            // מקריא את קובץ התמונה לדאטה של בייטים (ArrImage)
            CreateMap<Professionals, ProfessionalsDto>().ForMember("ArrImage", x => x.MapFrom(y => File.ReadAllBytes(path + y.ImageUrls)));
            // 🟢 מיפוי של ProfessionalsDto ➡️ Professionals
            // שומר רק את שם הקובץ (FileName) לתוך ImageUrls
            CreateMap<ProfessionalsDto, Professionals>().ForMember("ImageUrls", x => x.MapFrom(y => y.fileImage.FileName));
            // ✅ מיפוי של User ➡️ UserDto (מיפוי פשוט, אין בו תמונה כרגע)
            CreateMap<User, UserDto>();

            // ✅ מיפוי של UserDto ➡️ User
            CreateMap<UserDto, User>();
            // ✅ מיפוי של Category ➡️ CategoryDto (מיפוי פשוט, אין בו תמונה כרגע)
            CreateMap<Category, CategoryDto>();

            // ✅ מיפוי של CategoryDto ➡️ Category
            CreateMap<CategoryDto, Category>();
            // ✅ מיפוי של Comment ➡️ CommentDto (מיפוי פשוט, אין בו תמונה כרגע)
            CreateMap<Comment, CommentDto>();

            // ✅ מיפוי של CommentDto ➡️ Comment
            CreateMap<CommentDto, Comment>();


        }
    }
}
