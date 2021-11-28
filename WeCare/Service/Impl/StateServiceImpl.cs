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
                    StateSystolicPressure = new Random().Next(115, 131),
                    StateDiastolicPressure = new Random().Next(75, 91),
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
            var res = pContext.States.Find(stateId);
            if (res != null)
            {
                return pMapper.Map<StateDto>(pContext.States.
                    Single(x => x.StateId == stateId));
            }
            else return new StateDto();
        }

        public DataCollection<StateSimpleDto> GetSimpleByPatientId(int patientId, int page, int take)
        {
            return pMapper.Map<DataCollection<StateSimpleDto>>(pContext.States.
                Where(x => x.PatientId == patientId).
                OrderByDescending(x => x.StateId).
                AsQueryable().Paged(page, take)
                );
        }

        //
        public List<StateSimpleDto> GetTodayStates(int patientId, int page, int take)
        {
            List<StateSimpleDto> fool = new List<StateSimpleDto>();
            var lis = DataStates(patientId, page, take);
            foreach (var ele in lis.Items) if (TimeHelper.checkDay(ele.StateDate, DateTime.Now)) fool.Add(ele);
            return fool;
        }

        public List<(string, int, int, int)> GetWeeklyStates(int patientId, int page, int take)
        {
            // Primero, obtener el dia de inicio de semana y calcular desde alli un rango
            var SoW = TimeHelper.getStartWeek(DateTime.Now.ToString());
            var EoW = TimeHelper.getEndWeek(SoW);
            //var EoW = DateTime.Now;
            List<StateSimpleDto> fool = new List<StateSimpleDto>();
            var lis = DataStates(patientId, page, take);
            foreach (var ele in lis.Items)
            {
                if (TimeHelper.checkWeek(ele.StateDate, SoW, EoW))
                {
                    fool.Add(ele);
                }
            }
            List<(string, int, int, int)> dateproms = new List<(string, int, int, int)>();

            foreach (var dum in fool.Select(x => x.StateDate))
            {
                var prombpmday = 0; var promsisday = 0; var promdisday = 0; var count = 0;
                foreach (var foli in fool)
                {
                    if (foli.StateDate == dum) {
                        prombpmday += foli.StateBPM;
                        promsisday += foli.StateSystolicPressure;
                        promdisday += foli.StateDiastolicPressure;
                        count++; 
                    }
                }
                dateproms.Add((dum, prombpmday / count, promsisday / count, promdisday / count));
            }
            return dateproms.Distinct().ToList();
            //return dateproms.Distinct().ToList();
        }

        public List<(string, int, int, int)> GetMonthlyStates(int patientId, int page, int take)
        {
            List<(string, int, int, int)> weekproms = new List<(string, int, int, int)>();
            var count = 0;
            var evaluating = new DateTime();
            var ids = new DateTime();
            var fds = new DateTime();
            var year = DateTime.Now.Year;
            var mon = DateTime.Now.Month;
            for (var tempdate = 1; tempdate <= DateTime.Now.Day; tempdate += 7)
            {
                count++;
                evaluating = new DateTime(year, mon, tempdate);
                if (tempdate == 1)
                {
                    ids = new DateTime(year, mon, tempdate);
                    fds = TimeHelper.getEndWeek(TimeHelper.getStartWeek(evaluating.ToString()));
                }
                else
                {
                    ids = TimeHelper.getStartWeek(evaluating.ToString());
                    fds = TimeHelper.getEndWeek(ids);
                }
                var dum = WeekEvents(patientId, ids, fds, page, take);
                weekproms.Add(("Week " + count, dum.Item1, dum.Item2, dum.Item3));
            }
            return weekproms;
        }
        //
        private DataCollection<StateSimpleDto> DataStates(int patientId, int page, int take)
        {
            return pMapper.Map<DataCollection<StateSimpleDto>>(pContext.States.
                Where(x => x.PatientId == patientId).
                OrderByDescending(x => x.StateId).
                AsQueryable().Paged(page, take)
                );
        }
        private List<StateSimpleDto> DataMonthly(int patientId, int page, int take)
        {
            List<StateSimpleDto> fool = new List<StateSimpleDto>();
            var lis = DataStates(patientId, page, take);
            foreach (var ele in lis.Items)
            {
                var eMon = DateTime.Parse(ele.StateDate);
                if (eMon.Year == DateTime.Now.Year && eMon.Month == DateTime.Now.Month)
                {
                    fool.Add(ele);
                }
            }
            return fool;
        }
        private (int,int, int) WeekEvents(int patientId, DateTime StartWeek, DateTime EndWeek, int page, int take)
        {
            List<StateSimpleDto> foo = new List<StateSimpleDto>();
            var loo = DataMonthly(patientId, page, take);
            foreach (var ele in loo)
            {
                var eDay = DateTime.Parse(ele.StateDate);
                if (eDay >= StartWeek && eDay <= EndWeek)
                {
                    foo.Add(ele);
                }
            }
            if (foo.Count > 0)
            {
                List<(string, int, int, int)> dateproms = new List<(string, int, int, int)>();
                foreach (var dum in foo.Select(x => x.StateDate))
                {
                    var prombpm = 0; var promsis = 0; var promdis = 0; var count = 0;
                    foreach (var foli in foo)
                    {
                        if (foli.StateDate == dum) {
                            prombpm += foli.StateBPM;
                            promsis += foli.StateSystolicPressure; 
                            promdis = foli.StateDiastolicPressure; 
                            count++; 
                        }
                    }
                    dateproms.Add((dum, prombpm / count, promsis / count, promdis / count));
                }
                var prombpmsem = 0; var promsissem = 0; var promdissem = 0; var countsem = 0;
                foreach (var s in dateproms)
                {
                    countsem++;
                    prombpmsem += s.Item2; // BPM
                    promsissem += s.Item3; // Systolic
                    promdissem += s.Item4; // Diastolic
                }
                return (prombpmsem / countsem, promsissem / countsem, promdissem / countsem);
            }
            else return (0,0,0);
        }
    }
}
