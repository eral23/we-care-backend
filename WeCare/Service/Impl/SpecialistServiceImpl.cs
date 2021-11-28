using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Entities;
using WeCare.Persistance;
using WeCare.Util;

namespace WeCare.Service.Impl
{
    public class SpecialistServiceImpl : SpecialistService
    {
        private readonly ApplicationDbContext pContext;
        private readonly IMapper pMapper;
        private static int pid;

        public SpecialistServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            pid = 0;
            pContext = context;
            pMapper = mapper;
        }
        public SpecialistDto Create(SpecialistCreateDto model)
        {
            var entry = new Specialist
            {
                SpecialistName = model.SpecialistName,
                SpecialistLastname = model.SpecialistLastname,
                SpecialistEmail = model.SpecialistEmail,
                SpecialistArea = model.SpecialistArea,
                SpecialistTuitionNumber = model.SpecialistTuitionNumber,
                SpecialistId = pid++
            };
            pContext.Specialists.Add(entry);
            pContext.SaveChanges();
            return pMapper.Map<SpecialistDto>(entry);
        }

        public DataCollection<SpecialistDto> GetAll(int page, int take)
        {
            return pMapper.Map<DataCollection<SpecialistDto>>(pContext.Specialists.Include(x => x.Requisitions).
                OrderByDescending(x => x.SpecialistId).AsQueryable().Paged(page,take)
                );
        }

        public DataCollection<SpecialistSimpleDto> GetAllSimple(int page, int take)
        {
            return pMapper.Map<DataCollection<SpecialistSimpleDto>>(pContext.Specialists.
                OrderByDescending(x => x.SpecialistId).AsQueryable().Paged(page, take)
                );
        }

        public SpecialistDto GetById(int specialistId)
        {
            var res = pContext.Specialists.Find(specialistId);
            if (res != null)
            {
                return pMapper.Map<SpecialistDto>(pContext.Specialists.
                    Single(x => x.SpecialistId == specialistId));
            }
            else return new SpecialistDto();
        }

        public SpecialistSimpleDto GetByEmail(string specialistEmail)
        {
            var res = pContext.Specialists.Where(x => x.SpecialistEmail == specialistEmail);
            if (res != null)
            {
                return pMapper.Map<SpecialistSimpleDto>(pContext.Specialists.
                    Single(x => x.SpecialistEmail == specialistEmail));
            }
            else return new SpecialistSimpleDto();
        }
    }
}
