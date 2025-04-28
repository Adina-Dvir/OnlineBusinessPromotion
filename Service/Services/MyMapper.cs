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
    public class MyMapper:Profile
    {
        //מביא לנו את הניתוב ביחסי של הפרויקט במחשב עד הפרויקט ואז אני צריכה להכניס לו שיכנס לתמונות
        string path = Path.Combine(Environment.CurrentDirectory, "Images\\");

        public MyMapper() {


            CreateMap<Professionals, ProfessionalsDto>().ForMember("ArrImage", x => x.MapFrom(y => File.ReadAllBytes(path + y.ImageUrls)));
            CreateMap<ProfessionalsDto, Professionals>().ForMember("ImageUrls", x => x.MapFrom(y => y.fileImage.FileName));

        }
    }
}
