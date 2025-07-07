// Service.Interfaces/IProfessionalService.cs
using Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProfessionalService
    {
        Task<ProfessionalsDto> GetById(int id);
        Task<List<ProfessionalsDto>> GetAll();
        Task<ProfessionalsDto> AddItem(ProfessionalFormDto item);
        Task UpdateItem(int id, ProfessionalFormDto item);
        Task DeleteItem(int id);
    }
}
