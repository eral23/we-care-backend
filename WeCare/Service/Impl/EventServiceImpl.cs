using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Util;
using WeCare.Persistance;
using AutoMapper;
using WeCare.Entities;

namespace WeCare.Service.Impl
{
    public class EventServiceImpl : EventService
    {
        private readonly ApplicationDbContext pContext;
        private readonly IMapper pMapper;
        private static int pid;
        public EventServiceImpl(ApplicationDbContext context, IMapper mapper)
        {
            pid = 0;
            pContext = context;
            pMapper = mapper;
        }
        public EventDto Create(EventCreateDto model)
        {
            Patient patient = pContext.Patients.Single(x => x.PatientId == model.PatientId);
            var entry = new Event
            {
                EventName = model.EventName,
                EventDescription = model.EventDescription,
                EventScore = model.EventScore,
                EventResult = model.EventResult,
                EventDetail = model.EventDetail,
                EventDate = DateTime.Now.ToString("yyyy-MM-dd"),
                EventTime = DateTime.Now.ToString("hh:mm:ss tt"),
                PatientId = model.PatientId,
                Patient = patient,
                EventId = pid++
            };
            pContext.Events.Add(entry);
            pContext.SaveChanges();
            return pMapper.Map<EventDto>(entry);
        }

        public DataCollection<EventDto> GetAll(int page, int take)
        {
            return pMapper.Map<DataCollection<EventDto>>(pContext.Events.Include(x => x.Patient).
                OrderByDescending(x => x.EventId).AsQueryable().Paged(page, take)
                );
        }

        public DataCollection<EventSimpleDto> GetAllSimple(int page, int take)
        {
            return pMapper.Map<DataCollection<EventSimpleDto>>(pContext.Events.
                OrderByDescending(x => x.EventId).AsQueryable().Paged(page, take)
                );
        }

        public EventDto GetById(int eventId)
        {
            return pMapper.Map<EventDto>(pContext.Events.
                Single(x => x.EventId == eventId));
        }

        public DataCollection<EventSimpleDto> GetSimpleByPatientId(int patientId, int page, int take)
        {
            return pMapper.Map<DataCollection<EventSimpleDto>>(pContext.Events.
                Where(x => x.PatientId == patientId).OrderByDescending(x => x.EventId).
                AsQueryable().Paged(page, take)
                );
        }
    }
}
