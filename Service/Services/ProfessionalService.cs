using AutoMapper;
using Common.Dto;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Entities;
using Repository.Entities.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Service.Services
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IRepository<Professionals> _professionalsRepository;
        private readonly IRepository<ProfessionalImages> _imagesRepository;
        private readonly IMapper _mapper;
        private readonly IContext _context;
        public ProfessionalService(
            IRepository<Professionals> professionalsRepository,
            IRepository<ProfessionalImages> imagesRepository,
            IMapper mapper, IContext context)
        {
            _professionalsRepository = professionalsRepository;
            _imagesRepository = imagesRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProfessionalsDto> AddItem(ProfessionalFormDto item)
        {
            var entity = _mapper.Map<Professionals>(item);

            // שמירת התמונות בתוך ImageData
            if (item.fileImages != null && item.fileImages.Any())
            {
                foreach (var formFile in item.fileImages)
                {
                    if (formFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await formFile.CopyToAsync(ms);

                            var image = new ProfessionalImages
                            {
                                ImageData = ms.ToArray(),
                                FileName = formFile.FileName,
                                Professional = entity
                            };

                            entity.Images.Add(image);
                        }
                    }
                }
            }

            var added = await _professionalsRepository.AddItem(entity);
            return _mapper.Map<ProfessionalsDto>(added);
        }


        public async Task DeleteItem(int id)
        {
            await _professionalsRepository.DeleteItem(id);
        }

        public async Task<List<ProfessionalsDto>> GetAll()
        {
            var list = await _professionalsRepository.GetAll();
            return _mapper.Map<List<ProfessionalsDto>>(list);
        }

        public async Task<ProfessionalsDto> GetById(int id)
        {
            var entity = await _professionalsRepository.GetById(id);
            return _mapper.Map<ProfessionalsDto>(entity);
        }

        public async Task UpdateItem(int id, ProfessionalFormDto item)
        {
            var entity = _mapper.Map<Professionals>(item);
            await _professionalsRepository.UpdateItem(id, entity);
        }
        public async Task<List<ProfessionalsDto>> GetProfessionalsByCategory(int categoryId)
        {
            var professionals = await _context.Professionals
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            return _mapper.Map<List<ProfessionalsDto>>(professionals);
        }



    }
}
