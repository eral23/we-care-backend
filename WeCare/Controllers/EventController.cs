using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Service;
using WeCare.Util;

namespace WeCare.Controllers
{
    [Route("events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService pEventService;

        public EventController(EventService eventService)
        {
            pEventService = eventService;
        }

        [HttpGet]
        public ActionResult<DataCollection<EventDto>> GetAll(int page = 1, int take = 20)
        {
            return pEventService.GetAll(page, take);
        }

        [HttpGet("simple")]
        public ActionResult<DataCollection<EventSimpleDto>> GetAllSimple(int page = 1, int take = 20)
        {
            return pEventService.GetAllSimple(page, take);
        }

        [HttpGet("simple/{patient_id}")]
        public ActionResult<DataCollection<EventSimpleDto>> GetSimpleByPatienttId(int patient_id, int page = 1, int take = 20)
        {
            return pEventService.GetSimpleByPatientId(patient_id, page, take);
        }

        [HttpGet("{id}")]
        public ActionResult<EventDto> GetById(int id)
        {
            return pEventService.GetById(id);
        }

        [HttpPost]
        public ActionResult Create(EventCreateDto evento) // Careful: "event" is a reserved word
        {
            var rs = pEventService.Create(evento);
            if (rs.EventName != null) return Ok();
            else return BadRequest();
        }

        [HttpPost("simple")]
        public ActionResult CreateSimple(EventSimpleCreateDto evento)
        {
            var rs = pEventService.CreateSimple(evento);
            if (rs.EventName != null) return Ok(rs);
            else return BadRequest();
        }

        [HttpGet("today/{patient_id}")]
        public ActionResult<List<EventSimpleDto>> GetTodayByPatienttId(int patient_id, int page = 1, int take = 20)
        {
            return pEventService.GetTodayEvents(patient_id, page, take);
        }

        [HttpGet("weekly/{patient_id}")]
        public ActionResult<List<(string, int)>> GetWeeklyByPatienttId(int patient_id, int page = 1, int take = 20)
        {
            return pEventService.GetWeeklyEvents(patient_id, page, take);
        }
        [HttpGet("monthly/{patient_id}")]
        public ActionResult<List<(string, int)>> GetMonthlyByPatienttId(int patient_id, int page = 1, int take = 20)
        {
            return pEventService.GetMonthlyEvents(patient_id, page, take);
        }
    }
}
