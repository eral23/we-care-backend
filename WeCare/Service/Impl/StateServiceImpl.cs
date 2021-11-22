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
    public class StateServiceImpl : StateService
    {
        private readonly ApplicationDbContext pContext;
        private readonly IMapper pMapper;
        private static int pid;
        public StateServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            pid = 0;
            pContext = context;
            pMapper = mapper;
        }
        public StateDto Create(StateCreateDto model)
        {
            Patient patient = pContext.Patients.Find(model.PatientId);
            if (patient != null)
            {
                var entry = new State
                {
                    StateBPM = model.StateBPM,
                    StateSystolicPressure = model.StateSystolicPressure,
                    StateDiastolicPressure = model.StateDiastolicPressure,
                    StateDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    StateTime = DateTime.Now.ToString("hh:mm:ss"),
                    PatientId = model.PatientId,
                    Patient = patient,
                    StateId = pid++
                };
                pContext.States.Add(entry);
                pContext.SaveChanges();
                return pMapper.Map<StateDto>(entry);
            }
            else return new StateDto();
        }

        public StateDto CreateSimple(StateSimpleCreateDto model)
        {
            Patient patient = pContext.Patients.Find(model.PatientId);
            if (patient != null)
            {
                var entry = new State
                {
                    StateBPM = new Random().Next(50, 111),
                    StateSystolicPressure = new Random().Next(),
                    StateDiastolicPressure = new Random().Next(),
                    StateDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    StateTime = DateTime.Now.ToString("hh:mm:ss"),
                    PatientId = model.PatientId,
                    Patient = patient,
                    StateId = pid++
                };
                pContext.States.Add(entry);
                pContext.SaveChanges();
                return pMapper.Map<StateDto>(entry);
            }
            else return new StateDto();
        }

        public DataCollection<StateDto> GetAll(int page, int take)
        {
            return pMapper.Map<DataCollection<StateDto>>(pContext.States.
                Include(x => x.Patient).OrderByDescending(x => x.StateId).AsQueryable().
                Paged(page, take));
        }

        public DataCollection<StateSimpleDto> GetAllSimple(int page, int take)
        {
            return pMapper.Map<DataCollection<StateSimpleDto>>(pContext.States.
                OrderByDescending(x => x.StateId).AsQueryable().
                Paged(page, take));
        }

        public StateDto GetById(int stateId)
        {
            return pMapper.Map<StateDto>(pContext.States.
                Single(x => x.StateId == stateId));
        }

        public DataCollection<StateSimpleDto> GetSimpleByPatientId(int patientId, int page, int take)
        {
            return pMapper.Map<DataCollection<StateSimpleDto>>(pContext.States.
                Where(x => x.PatientId == patientId).
                OrderByDescending(x => x.StateId).
                AsQueryable().Paged(page, take)
                );
        }
    }
}
