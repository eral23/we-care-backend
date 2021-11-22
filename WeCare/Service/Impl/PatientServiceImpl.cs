using AutoMapper;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Entities;
using WeCare.Persistance;
using WeCare.Util;

namespace WeCare.Service.Impl
{
    public class PatientServiceImpl : PatientService
    {
        private readonly ApplicationDbContext pContext;
        private readonly IMapper pMapper;
        private static int pid;
        public PatientServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            pid = 0;
            pContext = context;
            pMapper = mapper;
        }
        public PatientDto Create(PatientCreateDto model)
        {           
            var entry = new Patient
            {
                PatientName = model.PatientName,
                PatientLastname = model.PatientLastname,
                PatientEmail = model.PatientEmail,
                PatientLinked =  false,
                PatientId = pid++
            };
            pContext.Patients.Add(entry);
            pContext.SaveChanges();
            return pMapper.Map<PatientDto>(entry);
        }

        public DataCollection<PatientDto> GetAll(int page, int take)
        {
            return pMapper.Map<DataCollection<PatientDto>>(pContext.Patients.Include(x => x.Requisitions).
                OrderByDescending(x => x.PatientId).AsQueryable().Paged(page, take)
                );
        }

        public DataCollection<PatientSimpleDto> GetAllSimple(int page, int take)
        {
            return pMapper.Map<DataCollection<PatientSimpleDto>>(pContext.Patients.
                OrderByDescending(x => x.PatientId).AsQueryable().Paged(page, take)
                );
        }

        public PatientSimpleDto GetByEmail(string patientEmail)
        {
            return pMapper.Map<PatientSimpleDto>(pContext.Patients.
                Single(x => x.PatientEmail == patientEmail));
        }

        public PatientDto GetById(int patientId)
        {
            return pMapper.Map<PatientDto>(pContext.Patients.
                Single(x => x.PatientId == patientId));
        }
    }
}
