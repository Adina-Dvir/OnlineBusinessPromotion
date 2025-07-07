using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Entities.Entities;

namespace Service.Mapping;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // מיפוי תמונות מקצוען ל־DTO של תמונות מקצוען
        CreateMap<ProfessionalImages, ProfessionalImageDto>();

        // מיפוי מקצוענים ל־DTO שלהם, כולל מיפוי רשימת התמונות
        CreateMap<Professionals, ProfessionalsDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        // מיפוי טופס מקצוען ליישות מקצוען, מתעלם מתמונות כי לא שולחים אותן בטופס
        CreateMap<ProfessionalFormDto, Professionals>()
            .ForMember(dest => dest.Images, opt => opt.Ignore());
    }

}
