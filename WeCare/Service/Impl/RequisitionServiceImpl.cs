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
    public class RequisitionServiceImpl : RequisitionService
    {
        private readonly ApplicationDbContext pContext;
        private readonly IMapper pMapper;
        private static int pid;

        public RequisitionServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            pid = 0;
            pContext = context;
            pMapper = mapper;
        }
        public RequisitionDto Create(RequisitionCreateDto model)
        {
            Patient patient = pContext.Patients.Find(model.PatientId);
            Specialist specialist = pContext.Specialists.Find(model.SpecialistId);
            if (patient != null && specialist != null)
            {
                var entry = new Requisition
                {
                    RequisitionStatus = "Pending",
                    PatientId = model.PatientId,
                    Patient = patient,
                    SpecialistId = model.SpecialistId,
                    Specialist = specialist,
                    RequisitionId = pid++
                };
                pContext.Requisitions.Add(entry);
                pContext.SaveChanges();
                return pMapper.Map<RequisitionDto>(entry);
            }
            else return new RequisitionDto();
        }

        public DataCollection<RequisitionDto> GetAll(int page, int take)
        {
            return pMapper.Map<DataCollection<RequisitionDto>>(pContext.Requisitions.
                Include(x => x.Patient).Include(x => x.Specialist).OrderByDescending(x => x.RequisitionId).AsQueryable().
                Paged(page, take));
        }

        public DataCollection<RequisitionSimpleDto> GetAllSimple(int page, int take)
        {
            return pMapper.Map<DataCollection<RequisitionSimpleDto>>(pContext.Requisitions.
                OrderByDescending(x => x.RequisitionId).AsQueryable().
                Paged(page, take));
        }

        public RequisitionDto GetById(int requisitionId)
        {
            return pMapper.Map<RequisitionDto>(pContext.Requisitions.
                Single(x => x.RequisitionId == requisitionId));
        }

        public DataCollection<RequisitionSimpleDto> GetSimpleByPatientId(int patientId, int page, int take)
        {
            return pMapper.Map<DataCollection<RequisitionSimpleDto>>(pContext.Requisitions.
                Where(x => x.PatientId == patientId).
                OrderByDescending(x => x.RequisitionId).
                AsQueryable().Paged(page, take)
                );
        }

        public DataCollection<RequisitionSimpleDto> GetSimpleBySpecialistId(int specialistId, int page, int take)
        {
            return pMapper.Map<DataCollection<RequisitionSimpleDto>>(pContext.Requisitions.
                Where(x => x.SpecialistId == specialistId).
                OrderByDescending(x => x.RequisitionId).
                AsQueryable().Paged(page, take)
                );
        }

        public RequisitionDto Update(RequisitionUpdateDto model)
        {
            // Para actualizar el estado, implementado en esta semana
            throw new NotImplementedException();
        }
    }
}
