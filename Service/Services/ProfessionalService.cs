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
            var entity = await _context.Professionals
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.ProfessionalId == id);

            if (entity == null)
                throw new Exception("לא נמצא");

            var dto = new ProfessionalsDto
            {
                ProfessionalId = entity.ProfessionalId,
                ProfessionalName = entity.ProfessionalName,
                ProfessionalAdress = entity.ProfessionalAdress,
                ProfessionalDescription = entity.ProfessionalDescription,
                PriceRange = entity.PriceRange,
                ProfessionalPhone = entity.ProfessionalPhone,
                ProfessionalEmail = entity.ProfessionalEmail,
                Subject = entity.Subject,
                Years = entity.Years,
                ProfessionalPassword = entity.ProfessionalPassword,
                UploadDate = entity.UploadDate,
                ProfessionalPlace = entity.ProfessionalPlace,
                Profile = entity.Profile,
                City = entity.City,
                CategoryId = entity.CategoryId,
                Images = entity.Images?
                .Where(img => img.ImageData != null && img.ImageData.Length > 0)
                .Select(img => new ProfessionalImageDto
                {
                    ImageId = img.ImageId,
                    FileName = img.FileName,
                    ProfessionalId = img.ProfessionalId,
                    ImageBase64 = $"data:image/jpeg;base64,{Convert.ToBase64String(img.ImageData)}"
                }).ToList()

            };

            return dto;
        }


        public async Task UpdateItem(int id, ProfessionalFormDto professional)
        {
            if (!Validation.IsValidName(professional.ProfessionalName))
                throw new ArgumentException("שם לא תקין");

            if (!Validation.IsValidPhoneNumber(professional.ProfessionalPhone))
                throw new ArgumentException("מספר טלפון לא תקין");

            if (!Validation.IsValidEmail(professional.ProfessionalEmail))
                throw new ArgumentException("אימייל לא תקין");

            if (!Validation.IsValidAddress(professional.ProfessionalAdress))
                throw new ArgumentException("כתובת לא תקינה");
            var entity = _mapper.Map<Professionals>(professional);
            await _professionalsRepository.UpdateItem(id, entity);
        }
        public async Task<List<ProfessionalsDto>> GetProfessionalsByCategory(int categoryId)
        {
            var professionals = await _context.Professionals
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            return _mapper.Map<List<ProfessionalsDto>>(professionals);
        }

        public async Task<ProfessionalsDto> AddItem(ProfessionalFormDto professional)
        {
            if (!Validation.IsValidName(professional.ProfessionalName))
                    throw new ArgumentException("שם לא תקין");

                if (!Validation.IsValidPhoneNumber(professional.ProfessionalPhone))
                    throw new ArgumentException("מספר טלפון לא תקין");

                if (!Validation.IsValidEmail(professional.ProfessionalEmail))
                    throw new ArgumentException("אימייל לא תקין");

                if (!Validation.IsValidAddress(professional.ProfessionalAdress))
                    throw new ArgumentException("כתובת לא תקינה");

                var entity = _mapper.Map<Professionals>(professional);

                if (professional.fileImages != null && professional.fileImages.Any())
                {
                    foreach (var formFile in professional.fileImages)
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
        }
    }

