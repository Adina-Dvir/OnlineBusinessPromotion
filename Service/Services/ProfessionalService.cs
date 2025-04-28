using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProfessionalService : IService<ProfessionalsDto>
    {
        private readonly IRepository<Professionals> repository;
        private readonly IMapper mapper;
        public ProfessionalService()
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public ProfessionalService(IRepository<Professionals> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        public ProfessionalsDto AddItem(ProfessionalsDto item)
        {
            Professionals p = repository.AddItem(mapper.Map<ProfessionalsDto, Professionals>(item));

            return mapper.Map<Professionals, ProfessionalsDto>(p);
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<ProfessionalsDto> GetAll()
        {
            return mapper.Map<List<Professionals>, List<ProfessionalsDto>>(repository.GetAll());
        }

        public ProfessionalsDto GetById(int id)
        {
            return mapper.Map<Professionals, ProfessionalsDto>(repository.GetById(id));
        }

        public void UpdateItem(int id, ProfessionalsDto item)
        {
            repository.UpdateItem(id, mapper.Map<ProfessionalsDto, Professionals>(item));
        }
    }
}
