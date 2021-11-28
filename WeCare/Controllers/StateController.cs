using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Service;
using WeCare.Util;

namespace WeCare.Controllers
{
    [Route("states")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateService pStateService;
        public StateController(StateService stateService)
        {
            pStateService = stateService;
        }

        [HttpGet]
        public ActionResult<DataCollection<StateDto>> GetAll(int page = 1, int take = 20)
        {
            return pStateService.GetAll(page, take);
        }

        [HttpGet("simple")]
        public ActionResult<DataCollection<StateSimpleDto>> GetAllSimple(int page = 1, int take = 20)
        {
            return pStateService.GetAllSimple(page, take);
        }

        [HttpGet("simple/{patient_id}")]
        public ActionResult<DataCollection<StateSimpleDto>> GetSimpleByPatienttId(int patient_id, int page = 1, int take = 20)
        {
            return pStateService.GetSimpleByPatientId(patient_id, page, take);
        }

        [HttpGet("{id}")]
        public ActionResult<StateDto> GetById(int id)
        {
            var rs = pStateService.GetById(id);
            if (rs.StateId != 0) return rs;
            else return BadRequest(rs);
        }

        [HttpPost]
        public ActionResult Create(StateCreateDto state) 
        {
            var rs = pStateService.Create(state);
            if (rs.StateId != 0) return Ok(JObject.Parse("{stateId:" + rs.StateId + "}"));
            else return BadRequest(JObject.Parse("{stateId:" + rs.StateId + "}"));
        }

        [HttpPost("simple")]
        public ActionResult CreateSimple(StateSimpleCreateDto state)
        {
            var rs = pStateService.CreateSimple(state);
            if (rs.StateId != 0) return Ok(JObject.Parse("{stateId:" + rs.StateId + "}"));
            else return BadRequest(JObject.Parse("{stateId:" + rs.StateId + "}"));
        }
        [HttpGet("today/{patient_id}")]
        public ActionResult<List<StateSimpleDto>> GetTodayByPatienttId(int patient_id, int page = 1, int take = 20)
        {
            return pStateService.GetTodayStates(patient_id, page, take);
        }

        [HttpGet("weekly/{patient_id}")]
        public ActionResult<List<(string, int, int, int)>> GetWeeklyByPatienttId(int patient_id, int page = 1, int take = 20)
        {
            return pStateService.GetWeeklyStates(patient_id, page, take);
        }
        [HttpGet("monthly/{patient_id}")]
        public ActionResult<List<(string, int, int, int)>> GetMonthlyByPatienttId(int patient_id, int page = 1, int take = 20)
        {
            return pStateService.GetMonthlyStates(patient_id, page, take);
        }
    }
}
